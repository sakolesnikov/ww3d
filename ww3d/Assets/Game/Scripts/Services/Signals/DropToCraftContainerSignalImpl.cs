using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToCraftContainerSignalImpl : DefaultDropSignal<DropToCraftContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Drop(ref Entity lootEntity) {
        var player = world.GetPlayer();
        player.AddRelation(new CraftRelation { Entity = lootEntity });
    }

}

public abstract class DefaultDropSignal<TDropSignal> : GenericSignal<TDropSignal> where TDropSignal : struct, ITransform {

    protected abstract void Drop(ref Entity lootEntity);

    protected override void Signal(Signal<TDropSignal> signal) {
        var lootEntity = signal.Entity;
        var newItemContainer = signal.Event.Transform.GetComponent<IItemContainer>();
        if (newItemContainer.IsAvailableSlot()) {
            lootEntity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
        }

        Drop(ref lootEntity);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}