using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class CloseInventorySignalImpl : GenericSignal<CloseInventorySignal> {

    [Inject]
    private readonly EntityStore world;
    
    protected override void Signal(Signal<CloseInventorySignal> signal) {
        var invWnd = world.GetInventoryWnd();
        var canvas = world.GetCanvas();
        var inventoryRelations = invWnd.GetRelations<ShowsRelation>();
        using (ListPool<Entity>.Get(out var list)) {
            foreach (var relation in inventoryRelations) {
                relation.Entity.GetTransform().SetParent(canvas.transform, false);
                relation.Entity.GetGameObject().SetActive(false);
                list.Add(relation.Entity);
            }

            for (var i = 0; i < list.Count; i++) {
                invWnd.RemoveRelation<ShowsRelation>(list[i]);
            }
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(InventoryWindowDef);

}