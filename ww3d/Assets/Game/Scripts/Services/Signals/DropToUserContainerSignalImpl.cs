using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToUserContainerSignalImpl : GenericSignal<DropToUserContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropToUserContainerSignal> signal) {
        var player = world.GetPlayer();
        var lootEntity = signal.Entity;
        lootEntity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
        player.AddRelation(new InventoryRelation { Entity = lootEntity });
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}