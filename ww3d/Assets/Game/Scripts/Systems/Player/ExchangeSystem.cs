using Friflo.Engine.ECS;
using Transform = UnityEngine.Transform;

[LevelScope]
public class ExchangeSystem : EntityListSystem<OpenExchangeRequest> {

    private Entity inventory;

    protected override void OnAddStore(EntityStore store) {
        inventory = store.GetExchange();
    }

    protected override bool CanProcess() => !inventory.IsNull;

    protected override void ProcessEntity(ref OpenExchangeRequest component, Entity player) {
        ref var openInventoryRequest = ref player.GetComponent<OpenExchangeRequest>();
        var containerRelations = openInventoryRequest.Target.GetRelations<ContainsRelation>();
        ref var exchangeComp = ref inventory.GetComponent<ExchangeComponent>();

        SetParent(ref containerRelations, ref inventory, exchangeComp.Container);

        player.RemoveComponent<OpenExchangeRequest>();
        inventory.AddComponent(new OpenedComponent { Value = openInventoryRequest.Target });
        inventory.GetComponent<TransformComponent>().Value.GetChild(0).gameObject.SetActive(true);
    }

    private void SetParent(ref Relations<ContainsRelation> relations, ref Entity inventory, Transform parent) {
        foreach (ref var relation in relations) {
            relation.Entity.GetComponent<TransformComponent>().Value.SetParent(parent, false);
            relation.Entity.GetGameObject().SetActive(true);
            inventory.AddRelation(new ShowsRelation { Entity = relation.Entity });
        }
    }

}