using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToCraftContainerSignalImpl : GenericSignal<DropToCraftContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropToCraftContainerSignal> signal) {
        var player = world.GetPlayer();
        var lootEntity = signal.Entity;
        lootEntity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
        player.AddRelation(new CraftRelation { Entity = lootEntity });
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}