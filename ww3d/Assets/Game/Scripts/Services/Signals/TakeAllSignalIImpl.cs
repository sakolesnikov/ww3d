using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class TakeAllSignalIImpl : GenericSignal<TakeAllSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<TakeAllSignal> signal) {
        var inventory = world.GetExchange();
        ref var exchangeComp = ref inventory.GetComponent<ExchangeComponent>();
        var inventoryRelations = inventory.GetRelations<ShowsRelation>();
        foreach (var relation in inventoryRelations) {
            relation.Entity.EmitSignal(new DropToUserContainerSignal { Transform = exchangeComp.Player.transform });
            relation.Entity.EmitSignal(new EndDragSignal());
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(ExchangeDef);

}