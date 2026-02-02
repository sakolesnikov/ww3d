using System;
using Cysharp.Threading.Tasks;
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
    [Inject]
    private readonly PlayerWalkState walkState;
    [Inject]
    private readonly PlayerRunState runState;
    [Inject]
    private readonly PlayerIdleState idleState;
    [Inject]
    private readonly PlayerConfig playerConfig;

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var playerDef = entity.GetComponent<DefinitionComponent>().GetValue<PlayerDef>();
        entity.AddComponent(new SpeedComponent { Value = playerConfig.WalkSpeed });
        entity.AddComponent(new AnimatorComponent { Value = go.GetComponentInChildren<Animator>() });
        entity.AddComponent(new ActiveItemComponent { Index = 0 });

        var stateMachine = new StateMachine(entity);
        stateMachine.AddState(idleState);
        stateMachine.AddState(walkState);
        stateMachine.AddState(runState);
        stateMachine.SetCurrentState(typeof(PlayerIdleState));
        entity.AddComponent(new FSMComponent { Value = stateMachine, CurrentTask = UniTask.CompletedTask });
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