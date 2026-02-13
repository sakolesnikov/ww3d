using System;
using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class LootableInit : IEntityInitialization {

    [Inject]
    private readonly ItemProvider itemProvider;

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        entity.AddComponent(new ColliderComponent { Value = go.GetComponent<Collider>() });
        var lootable = go.GetComponent<Lootable>();
        foreach (var loot in lootable.Loots) {
            var itemEntity = itemProvider.GetItemEntity(loot);
            itemEntity.AddComponent(new TooltipComponent { Key = loot.EntityName });
            entity.AddRelation(new ContainsRelation { Entity = itemEntity });
        }
    }

    public Type getType() => typeof(LootableDef);

}