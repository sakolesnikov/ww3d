using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class ItemProvider : ISelfRegisterable {

    [Inject]
    [Key(nameof(PrefabItemDef))]
    private readonly ObjectPool<AbstractEntityMono> itemPool;
    [Inject]
    [Key(nameof(PrefabItemShadowDef))]
    private readonly ObjectPool<AbstractEntityMono> itemShadowPool;
    [Inject]
    private readonly EntityStore world;

    public Entity GetItemEntity(LootDef loot) {
        var entityMono = itemPool.Get();
        entityMono.gameObject.SetActive(false);
        entityMono.transform.SetParent(world.GetCanvas().transform, false);
        var prefabItemEntity = entityMono.GetEntity();
        var image = prefabItemEntity.GetComponent<ImageComponent>().Value;
        image.sprite = loot.Sprite;
        ref var defComp = ref prefabItemEntity.GetComponent<DefinitionComponent>();
        defComp.Value = loot;
        prefabItemEntity.GetComponent<EntityName>().value = loot.EntityType;
        return prefabItemEntity;
        // return default;
    }

    public Entity GetShadowEntity(LootDef loot) {
        var entityMono = itemShadowPool.Get();
        entityMono.transform.SetParent(world.GetCanvas().transform, false);
        var prefabItemShadowEntity = entityMono.GetEntity();
        var image = prefabItemShadowEntity.GetComponent<ImageComponent>().Value;
        image.sprite = loot.Sprite;
        ref var defComp = ref prefabItemShadowEntity.GetComponent<DefinitionComponent>();
        defComp.Value = loot;
        prefabItemShadowEntity.GetComponent<EntityName>().value = loot.EntityType;
        return prefabItemShadowEntity;
    }

    public void ReleaseItem(ref Entity shadowEntity) {
        var go = shadowEntity.GetComponent<GameObjectComponent>().Value;
        itemPool.Release(go.GetComponent<AbstractEntityMono>());
    }

    public void ReleaseShadow(ref Entity shadowEntity) {
        var go = shadowEntity.GetComponent<GameObjectComponent>().Value;
        itemShadowPool.Release(go.GetComponent<AbstractEntityMono>());
    }

}