using Friflo.Engine.ECS;
using VContainer;
using Transform = UnityEngine.Transform;

[LevelScope]
public class OpenInventorySignalImpl : GenericSignal<OpenInventorySignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<OpenInventorySignal> signal) {
        var player = world.GetPlayer();
        var playerRelations = player.GetRelations<ContainsRelation>();
        var invWnd = world.GetInventoryWnd();
        ref var invComp = ref invWnd.GetComponent<InventoryComponent>();
        SetParent(ref playerRelations, ref invWnd, invComp.PlayerContent);

        var craftRelations = player.GetRelations<CraftRelation>();
        foreach (var relation in craftRelations) {
            SetParent(relation.Entity, invWnd.GetComponent<CraftComponent>().Content);
        }
    }

    private void SetParent(ref Relations<ContainsRelation> relations, ref Entity invWnd, Transform parent) {
        foreach (ref var relation in relations) {
            relation.Entity.GetComponent<TransformComponent>().Value.SetParent(parent, false);
            relation.Entity.GetComponent<ParentTransformComponent>().Value = parent;
            relation.Entity.GetGameObject().SetActive(true);
            invWnd.AddRelation(new ShowsRelation { Entity = relation.Entity });
        }
    }

    private void SetParent(Entity lootEntity, Transform parent) {
        lootEntity.GetComponent<TransformComponent>().Value.SetParent(parent, false);
        lootEntity.GetGameObject().SetActive(true);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(ToolPanelDef);

}