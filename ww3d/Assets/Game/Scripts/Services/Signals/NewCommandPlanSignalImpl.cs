using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class NewCommandPlanSignalImpl : GenericSignal<NewCommandPlanSignal> {

    [Inject]
    private readonly ObjectPool<PooledCommandQueue> pool;

    protected override void Signal(Signal<NewCommandPlanSignal> signal) {
        var player = signal.Entity;

        if (player.HasComponent<ActiveCommandComponent>()) {
            player.RemoveComponent<ActiveCommandComponent>();
        }

        if (player.HasComponent<CommandPlanComponent>()) {
            ref var commandPlan = ref player.GetComponent<CommandPlanComponent>();
            pool.Release(commandPlan.Value);
            commandPlan.Value = signal.Event.Value;
            return;
        }

        player.AddComponent(new CommandPlanComponent { Value = signal.Event.Value });
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(PlayerDef);

}