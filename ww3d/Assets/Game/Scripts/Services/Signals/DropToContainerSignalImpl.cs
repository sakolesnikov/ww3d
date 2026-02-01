using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class DropToContainerSignalImpl : GenericSignal<DropToContainerSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<DropToContainerSignal> signal) {
        var player = world.GetPlayer();
        var lootEntity = signal.Entity;
        var containerTransform = signal.Event.Transform;
        lootEntity.GetComponent<ParentTransformComponent>().Value = containerTransform;
        if (player.TryGetRelation<ContainsRelation, Entity>(lootEntity, out var relation)) {
            player.RemoveRelation<ContainsRelation>(lootEntity);
            // var links = lootEntity.GetIncomingLinks<LeftHandComponent>();
            // if (links.Count > 0) {
            // player.RemoveComponent<LeftHandComponent>();
            // player.RemoveChild(lootEntity);
            // }
        }

        if (world.GetExchange() is { IsNull: false } exchange) {
            exchange.GetComponent<OpenedComponent>().Value.AddRelation(new ContainsRelation { Entity = lootEntity });
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}