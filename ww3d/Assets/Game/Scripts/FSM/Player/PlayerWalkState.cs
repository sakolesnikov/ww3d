using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class PlayerWalkState : DefaultState {

    [Inject]
    private readonly PlayerConfig playerConfig;

    public override UniTask Enter(Entity entity) {
        base.Enter(entity);
        ref var rotComp = ref entity.GetComponent<SpeedComponent>();
        rotComp.Value = playerConfig.WalkSpeed;
        rotComp.Rotation = playerConfig.RotationWalkSpeed;
        entity.GetComponent<AnimatorComponent>().CrossFade(Constants.ANIMATION_WALK);
        return UniTask.CompletedTask;
    }

    public override UniTask<Type> GetNextState(Entity entity) {
        if (entity.HasComponent<PathFollowerComponent>()) {
            ref var pathFollowerComp = ref entity.GetComponent<PathFollowerComponent>();
            var type = pathFollowerComp.MoveMode == MoveMode.Run ? typeof(PlayerRunState) : null;
            return UniTask.FromResult(type);
        }

        if (!entity.HasComponent<PathFollowerComponent>()) {
            return UniTask.FromResult(typeof(PlayerIdleState));
        }

        return UniTask.FromResult<Type>(null);
    }

}