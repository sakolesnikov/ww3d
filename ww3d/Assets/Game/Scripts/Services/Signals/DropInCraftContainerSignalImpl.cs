using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropInCraftContainerSignalImpl : GenericSignal<DropInCraftContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropInCraftContainerSignal> signal) {
        var player = world.GetPlayer();
        var containerTransform = signal.Event.Transform;
        var lootEntity = signal.Entity;
        lootEntity.GetComponent<ParentTransformComponent>().Value = containerTransform;

        player.AddRelation(new CraftRelation { Entity = lootEntity });
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}