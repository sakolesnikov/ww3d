using System;
using Cysharp.Threading.Tasks;
using Friflo.Engine.ECS;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using VContainer;

[LevelScope]
public class DoorOpenState : DefaultState {

    [Inject]
    private readonly DoorConfig doorConfig;
    private MotionHandle handler;

    public override UniTask Enter(Entity entity) {
        var transform = entity.GetComponent<DoorComponent>().Value;
        var startX = transform.localPosition.x;
        var endX = -doorConfig.Distance;
        var distance = Mathf.Abs(endX - startX);
        var duration = distance / doorConfig.Speed;
        handler = LMotion.Create(transform.localPosition.x, endX, duration).BindToLocalPositionX(transform);
        entity.AddComponent(new DoorMovementComponent());
        entity.RemoveTag<ClosedTag>();
        return UniTask.CompletedTask;
    }

    public override UniTask Exit(Entity entity) {
        handler.TryCancel();
        entity.RemoveComponent<DoorMovementComponent>();
        entity.AddTag<OpenedTag>();
        return UniTask.CompletedTask;
    }

    public override UniTask<Type> GetNextState(Entity entity) {
        if (entity.HasComponent<CloseDoorRequest>()) {
            entity.RemoveComponent<CloseDoorRequest>();
            return UniTask.FromResult(typeof(DoorCloseState));
        }

        if (!handler.IsPlaying()) {
            return UniTask.FromResult(typeof(DoorIdleState));
        }

        return UniTask.FromResult<Type>(null);
    }

}