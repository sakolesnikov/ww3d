using System;
using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class PlayerInit : IEntityInitialization, IDisposable {

    [Inject]
    private readonly ObjectPool<PooledCommandQueue> commandPool;
    [Inject]
    private readonly EntityStore world;

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var playerDef = entity.GetComponent<DefinitionComponent>().GetValue<PlayerDef>();
        entity.AddComponent(new SpeedComponent { Value = playerDef.Speed });
        entity.AddComponent(new AnimatorComponent { Value = go.GetComponentInChildren<Animator>() });
        entity.AddComponent(new ActiveItemComponent { Index = 0 });
        entity.OnComponentChanged += OnComponentChanged;
    }

    private void OnComponentChanged(ComponentChanged ev) {
        if (ev.Type == typeof(CommandPlanComponent) && ev.Action == ComponentChangedAction.Remove) {
            var planComp = ev.OldComponent<CommandPlanComponent>();
            commandPool.Release(planComp.Value);
            planComp.Value = null;
        }
    }

    public void Dispose() {
        if (world.GetPlayer() is { IsNull: false } p) {
            p.OnComponentChanged -= OnComponentChanged;
        }
    }

    public Type getType() => typeof(PlayerDef);

}