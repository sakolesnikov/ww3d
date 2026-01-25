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

        var ssm = new SimpleSmoothModifier
        {
            smoothType = SimpleSmoothModifier.SmoothType.Bezier,
            subdivisions = 3,
            bezierTangentLength = 0.15f
        };
        builder.RegisterInstance(ssm);

        builder.Register<InputSystem_Actions>(Lifetime.Singleton);
        builder.RegisterInstance(new ObjectPool<PooledCommandQueue>(() => new PooledCommandQueue(), actionOnRelease: cq => cq.Clear()));


        builder
            .Register(container =>
                container.Resolve<InventoryPoolFactory>().CreateItemPool(), Lifetime.Singleton)
            .Keyed(PrefabItemDef.Name);

        builder
            .Register(container =>
                container.Resolve<EntityMonoPoolProvider>().Create(GetConfig<UIConfig>().MessagePrefab), Lifetime.Singleton)
            .Keyed(MessageDef.Name);
    }

    private T GetConfig<T>() where T : ScriptableObject {
        foreach (var config in configs) {
            if (config is T result) {
                return result;
            }
        }

        return null;
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