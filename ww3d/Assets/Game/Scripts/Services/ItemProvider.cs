using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class ItemProvider : ISelfRegisterable {

    [Inject]
    [Key(nameof(PrefabItemDef))]
    private readonly ObjectPool<AbstractEntityMono> itemPool;
    [Inject]
    private readonly EntityStore world;
    [Inject]
    private readonly SignalRegistrationService signalRegistrationService;

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
    }

}