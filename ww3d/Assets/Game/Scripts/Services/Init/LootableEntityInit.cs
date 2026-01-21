using System;
using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class LootableEntityInit : IEntityInitialization {

    [Inject]
    private readonly ItemProvider itemProvider;

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var lootable = go.GetComponent<Lootable>();
        foreach (var loot in lootable.Loots) {
            var itemEntity = itemProvider.GetItemEntity(loot);
            entity.AddRelation(new ContainsRelation { Entity = itemEntity });
        }
    }

    public Type getType() => typeof(LootableEntityDef);

}