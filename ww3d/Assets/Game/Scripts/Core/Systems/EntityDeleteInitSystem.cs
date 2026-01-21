using Friflo.Engine.ECS;

/// <summary>
///     A critical cleanup system that intercepts entity deletion events to ensure proper
///     resource management and prevent memory leaks.
/// </summary>
/// <remarks>
///     This system subscribes to the global EntityStore.OnEntityDelete event to automatically
///     process entities before their final removal. It checks for two primary resource types:
///     <list type="bullet">
///         <item>
///             <term>Pooled Objects (PooledObjectComponent)</term>
///             <description>
///                 Calls Dispose() on the contained IDisposable to return the object
///                 (e.g., ParticleSystem, bullet) to its respective ObjectPool.
///             </description>
///         </item>
///         <item>
///             <term>GameObjects (GameObjectComponent)</term>
///             <description>
///                 Deactivates the associated GameObject via SetActive(false) for
///                 general entities that are not pool-managed but require deactivation instead of destruction.
///             </description>
///         </item>
///     </list>
///     The system ensures that all registered cleanup actions are performed reliably
///     when an entity is deleted from the world.
/// </remarks>
[LevelScope]
public class EntityDeleteInitSystem : IInitSystem, IDisposeSystem {

    public void Init(EntityStore world) {
        world.OnEntityDelete += EntityDeleteEvent;
    }

    public void Dispose(EntityStore world) {
        world.OnEntityDelete -= EntityDeleteEvent;
    }

    private void EntityDeleteEvent(EntityDelete action) {
        if (action.Entity.TryGetComponent(out PooledObjectComponent pooledObjectComponent)) {
            pooledObjectComponent.Value.Dispose();
        } else if (action.Entity.TryGetComponent(out GameObjectComponent component)) {
            component.Value.SetActive(false);
        }
    }

}