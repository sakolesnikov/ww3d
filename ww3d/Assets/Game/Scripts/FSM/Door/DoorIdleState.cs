using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;

[LevelScope]
public class DoorIdleState : DefaultState {

    public override UniTask<Type> GetNextState(Entity entity) {
        if (entity.HasComponent<OpenDoorRequest>()) {
            entity.RemoveComponent<OpenDoorRequest>();
            return UniTask.FromResult(typeof(DoorOpenState));
        }

        if (entity.HasComponent<CloseDoorRequest>()) {
            entity.RemoveComponent<CloseDoorRequest>();
            return UniTask.FromResult(typeof(DoorCloseState));
        }


        return UniTask.FromResult<Type>(null);
    }

}