using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class LevelLifetimeScope : DefaultLifetimeScope<LevelScopeAttribute> {

    [SerializeField]
    private ScriptableObject[] configs;

    protected override void Config(IContainerBuilder builder) {
        foreach (var scriptableObject in configs) {
            builder.RegisterInstance(scriptableObject).As(scriptableObject.GetType());
        }

        builder.Register<InputSystem_Actions>(Lifetime.Singleton);
        builder.RegisterInstance(new ObjectPool<PooledCommandQueue>(() => new PooledCommandQueue(), actionOnRelease: cq => cq.Clear()));


        builder
            .Register(container => container.Resolve<InventoryPoolFactory>().CreateItemPool(), Lifetime.Singleton)
            .Keyed(PrefabItemDef.Name);
    }

    private ObjectPool<ParticleSystem> CreateParticlePool(ParticleSystem prefab) {
        return new ObjectPool<ParticleSystem>(
            () => Instantiate(prefab),
            ps => ps.gameObject.SetActive(true),
            ps => {
                ps.Stop();
                ps.gameObject.SetActive(false);
            }
        );
    }

}