using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class PlayerRunState : DefaultState {

    [Inject]
    private readonly PlayerConfig playerConfig;

    public override UniTask Enter(Entity entity) {
        base.Enter(entity);
        ref var rotComp = ref entity.GetComponent<SpeedComponent>();
        rotComp.Value = playerConfig.RunSpeed;
        rotComp.Rotation = playerConfig.RotationRunSpeed;
        entity.GetComponent<AnimatorComponent>().CrossFade(Constants.ANIMATION_RUN);
        return UniTask.CompletedTask;
    }

    public override UniTask<Type> GetNextState(Entity entity) {
        if (!entity.HasComponent<PathFollowerComponent>()) {
            return UniTask.FromResult(typeof(PlayerIdleState));
        }

        ref var pathFollowerComp = ref entity.GetComponent<PathFollowerComponent>();
        if (pathFollowerComp.MoveMode == MoveMode.Walk) {
            return UniTask.FromResult(typeof(PlayerWalkState));
        }

        return UniTask.FromResult<Type>(null);
    }

}