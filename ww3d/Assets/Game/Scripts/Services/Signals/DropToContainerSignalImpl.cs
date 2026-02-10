using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToContainerSignalImpl : GenericSignal<DropToContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropToContainerSignal> signal) {
        var lootEntity = signal.Entity;
        signal.Entity.GetComponent<ParentTransformComponent>().Value = signal.Event.Transform;
        if (world.GetExchange() is { IsNull: false } exchange) {
            exchange.GetComponent<OpenedComponent>().Value.AddRelation(new ContainsRelation { Entity = lootEntity });
            exchange.AddRelation(new ShowsRelation { Entity = lootEntity });
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}