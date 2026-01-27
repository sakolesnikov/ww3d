using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using VContainer;

[LevelScope]
public class DoorCloseState : DefaultState {

    [Inject]
    private readonly DoorConfig doorConfig;
    private MotionHandle handler;

    public override UniTask Enter(Entity entity) {
        var transform = entity.GetComponent<DoorComponent>().Value;
        var endX = 0f;
        var distance = Mathf.Abs(endX - transform.localPosition.x);
        var duration = distance / doorConfig.Speed;
        handler = LMotion.Create(transform.localPosition.x, 0f, duration).BindToLocalPositionX(transform);
        entity.AddComponent(new DoorMovementComponent());
        entity.RemoveTag<OpenedTag>();
        return UniTask.CompletedTask;
    }

    public override UniTask Exit(Entity entity) {
        handler.TryCancel();
        entity.RemoveComponent<DoorMovementComponent>();
        entity.AddTag<ClosedTag>();
        return UniTask.CompletedTask;
    }

    public override UniTask<Type> GetNextState(Entity entity) {
        if (entity.HasComponent<OpenDoorRequest>()) {
            entity.RemoveComponent<OpenDoorRequest>();
            return UniTask.FromResult(typeof(DoorOpenState));
        }

        if (!handler.IsPlaying()) {
            return UniTask.FromResult(typeof(DoorIdleState));
        }

        return UniTask.FromResult<Type>(null);
    }

}