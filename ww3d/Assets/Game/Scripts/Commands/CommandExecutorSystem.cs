using Friflo.Engine.ECS;

[LevelScope]
public class CommandExecutorSystem : QueryUpdateSystem<CommandPlanComponent> {

    private readonly EntityList entityList = new();

    protected override void OnUpdate() {
        Query.Entities.ToEntityList(entityList);
        foreach (var entity in entityList) {
            ref var planComp = ref entity.GetComponent<CommandPlanComponent>();

            if (!TryGetOrInitCommand(entity, ref planComp, out var cmd)) {
                continue;
            }

            if (!cmd.IsFinished(entity)) {
                continue;
            }

            FinishCommand(entity, ref planComp, cmd);
        }
    }

    private bool TryGetOrInitCommand(
        Entity entity,
        ref CommandPlanComponent planComp,
        out ICommand cmd
    ) {
        cmd = null;

        if (entity.HasComponent<ActiveCommandComponent>()) {
            ref var active = ref entity.GetComponent<ActiveCommandComponent>();
            cmd = active.Value;
            return cmd != null;
        }

        if (planComp.Value.Count == 0) {
            return false;
        }

        cmd = planComp.Value.Dequeue();
        cmd.Init(entity);

        entity.AddComponent(new ActiveCommandComponent { Value = cmd });
        return true;
    }

    private void FinishCommand(
        Entity entity,
        ref CommandPlanComponent planComp,
        ICommand cmd
    ) {
        cmd.CleanUp(entity);

        if (planComp.Value.Count == 0) {
            entity.RemoveComponent<CommandPlanComponent>();
        }

        entity.RemoveComponent<ActiveCommandComponent>();
    }

}