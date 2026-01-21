using VContainer;
using VContainer.Unity;

[LevelScope]
public class PoolService : ISelfRegisterable {

    [Inject]
    private readonly IObjectResolver container;

    public AbstractEntityMono Instantiate(AbstractEntityMono prefab) {
        var entityMono2 = container.Instantiate(prefab);
        ref var entity = ref entityMono2.GetEntity();
        container.Resolve<EntityInitService>().Init(entity);
        container.Resolve<SignalRegistrationService>().Register(entity);
        return entityMono2;
    }

    public void ActionOnGet(AbstractEntityMono entityMono) {
        entityMono.gameObject.SetActive(true);
        entityMono.GetEntity().Enabled = true;
    }

    public void ActionOnDestroy(AbstractEntityMono entityMono) {
        container.Resolve<SignalRegistrationService>().Unregister(entityMono.GetEntity());
    }

    public void ActionOnRelease(AbstractEntityMono entityMono) {
        ref var entity = ref entityMono.GetEntity();
        entityMono.gameObject.SetActive(false);
        entity.Enabled = false;
    }

}