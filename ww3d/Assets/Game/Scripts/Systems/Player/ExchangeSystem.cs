using Friflo.Engine.ECS;
using Transform = UnityEngine.Transform;

[LevelScope]
public class ExchangeSystem : QueryUpdateSystem<OpenInventoryRequest, TappedEntityComponent> {

    private Entity inventory;
    private Entity player;
    private readonly EntityList entityList = new();

    protected override void OnAddStore(EntityStore store) {
        inventory = store.GetExchange();
        player = store.GetPlayer();
    }

    protected override void OnUpdate() {
        Query.Entities.ToEntityList(entityList);
        foreach (var entity in entityList) {
            ref var tappedComp = ref entity.GetComponent<TappedEntityComponent>();
            var containerRelations = tappedComp.Value.GetRelations<ContainsRelation>();
            ref var exchangeComp = ref inventory.GetComponent<ExchangeComponent>();

            SetParent(ref containerRelations, ref inventory, exchangeComp.Another.transform);
            var playerRelations = player.GetRelations<ContainsRelation>();
            SetParent(ref playerRelations, ref inventory, exchangeComp.Player.transform);


            entity.RemoveComponent<OpenInventoryRequest>();
            inventory.GetComponent<TransformComponent>().Value.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void SetParent(ref Relations<ContainsRelation> relations, ref Entity inventory, Transform parent) {
        foreach (ref var relation in relations) {
            relation.Entity.GetComponent<TransformComponent>().Value.SetParent(parent, false);
            relation.Entity.GetGameObject().SetActive(true);
            inventory.AddRelation(new ShowsRelation { Entity = relation.Entity });
        }
    }

}