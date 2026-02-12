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
    [Inject]
    private readonly IObjectResolver container;

    public Entity GetItemEntity(LootDef loot) => GetItemEntity(loot, false, false);

    public Entity GetItemEntity(LootDef loot, bool init, bool registerSignals) {
        var entityMono = itemPool.Get();
        var prefabItemEntity = entityMono.GetEntity();
        prefabItemEntity.GetComponent<DefinitionComponent>().Value = loot;
        prefabItemEntity.GetComponent<EntityName>().value = loot.EntityName;

        if (init) {
            container.Resolve<EntityInitService>().Init(prefabItemEntity);
        }

        if (registerSignals) {
            container.Resolve<SignalRegistrationService>().Register(prefabItemEntity);
        }

        entityMono.gameObject.SetActive(false);
        entityMono.transform.SetParent(world.GetCanvas().transform, false);

        var image = prefabItemEntity.GetComponent<ImageComponent>().Value;
        image.sprite = loot.Sprite;


        return prefabItemEntity;
    }

    public Entity GetShadowEntity(LootDef loot) {
        var entityMono = itemShadowPool.Get();
        entityMono.transform.SetParent(world.GetCanvas().transform, false);
        var prefabItemShadowEntity = entityMono.GetEntity();
        prefabItemShadowEntity.GetComponent<EntityName>().value = loot.EntityName;
        prefabItemShadowEntity.GetComponent<DefinitionComponent>().Value = loot;
        // container.Resolve<EntityInitService>().Init(prefabItemShadowEntity);
        // container.Resolve<SignalRegistrationService>().Register(prefabItemShadowEntity);

        var image = prefabItemShadowEntity.GetComponent<ImageComponent>().Value;
        image.sprite = loot.Sprite;
        return prefabItemShadowEntity;
    }

    public void ReleaseItem(ref Entity item) {
        var go = item.GetComponent<GameObjectComponent>().Value;
        itemPool.Release(go.GetComponent<AbstractEntityMono>());
    }

    public void ReleaseShadow(ref Entity shadowEntity) {
        var go = shadowEntity.GetComponent<GameObjectComponent>().Value;
        itemShadowPool.Release(go.GetComponent<AbstractEntityMono>());
    }

}