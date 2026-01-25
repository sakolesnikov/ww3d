using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;

[LevelScope]
public class PlayerIdleState : DefaultState {

    public override UniTask Enter(Entity entity) {
        base.Enter(entity);
        entity.GetComponent<AnimatorComponent>().CrossFade(Constants.ANIMATION_IDLE);
        return UniTask.CompletedTask;
    }

    public override UniTask<Type> GetNextState(Entity entity) {
        var has = entity.HasComponent<PathFollowerComponent>();
        if (has) {
            ref var pathFollowerComp = ref entity.GetComponent<PathFollowerComponent>();
            if (has && pathFollowerComp.StartedMoving) {
                var type = pathFollowerComp.MoveMode == MoveMode.Walk ? typeof(PlayerWalkState) : typeof(PlayerRunState);
                return UniTask.FromResult(type);
            }
        }

        return UniTask.FromResult<Type>(null);
    }

}