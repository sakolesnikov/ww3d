using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

public abstract class AbstractEntityMono : MonoBehaviour {

    [Inject]
    private readonly EntityProvider entityProvider;
    protected Entity Entity;
    protected EntityProvider EntityProvider => entityProvider;
    private IEntityAware[] entityAware;

    private void Awake() {
        CreateEntity();
        PostAwake();
        // GetComponentsInChildren is not allowed here, because if we have children they will be initialized with wrong entity
        // for example, ToolTip will have Canvas entity, because Tooltip entity is lower than Canvas in hierarchy
        entityAware = GetComponents<IEntityAware>();
        foreach (var ea in entityAware) {
            ea.OnEntityReady(ref Entity);
        }
    }

    private void OnDestroy() {
        ReleaseEntity();
    }

    protected virtual void PostAwake() { }

    public ref Entity GetEntity() => ref Entity;

    private void CreateEntity() {
        // Debug.Log($"gameObject.name [{gameObject.name}] gameObject.instanceId [{gameObject.GetInstanceID()}]");
        Entity = entityProvider.GetEntity(gameObject.GetInstanceID());
        Entity.AddComponent(new GameObjectComponent { Value = gameObject });
        Entity.AddComponent(new TransformComponent { Value = gameObject.transform });
        Entity.AddComponent(new OrderComponent { Value = int.MaxValue });
    }

    private void ReleaseEntity() {
        entityProvider.Release(gameObject.GetInstanceID());
    }

}