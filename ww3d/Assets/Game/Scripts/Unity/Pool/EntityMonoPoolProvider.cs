using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class EntityMonoPoolProvider : ISelfRegisterable {

    [Inject]
    private readonly PoolService poolService;

    public ObjectPool<AbstractEntityMono> Create(AbstractEntityMono entityMono) {
        return new ObjectPool<AbstractEntityMono>(
            () => poolService.Instantiate(entityMono),
            poolService.ActionOnGet,
            poolService.ActionOnRelease,
            poolService.ActionOnDestroy
        );
    }

}