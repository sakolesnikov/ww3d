using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class InventoryPoolFactory : ISelfRegisterable {

    [Inject]
    private readonly InventoryConfig invConfig;
    [Inject]
    private readonly PoolService poolService;

    public ObjectPool<AbstractEntityMono> CreateItemPool() {
        return new ObjectPool<AbstractEntityMono>(
            () => poolService.Instantiate(invConfig.ItemPrefab),
            poolService.ActionOnGet,
            poolService.ActionOnRelease,
            poolService.ActionOnDestroy
        );
    }

}