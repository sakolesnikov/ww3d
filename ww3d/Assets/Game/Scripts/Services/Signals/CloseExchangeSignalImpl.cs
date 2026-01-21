using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class CloseExchangeSignalImpl : GenericSignal<CloseExchangeSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<CloseExchangeSignal> signal) {
        var inventory = world.GetExchange();
        var canvas = world.GetCanvas();
        var inventoryRelations = inventory.GetRelations<ShowsRelation>();
        using (ListPool<Entity>.Get(out var list)) {
            foreach (var relation in inventoryRelations) {
                relation.Entity.GetTransform().SetParent(canvas.transform, false);
                relation.Entity.GetGameObject().SetActive(false);
                list.Add(relation.Entity);
            }

            for (var i = 0; i < list.Count; i++) {
                inventory.RemoveRelation<ShowsRelation>(list[i]);
            }
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(ExchangeDef);

}