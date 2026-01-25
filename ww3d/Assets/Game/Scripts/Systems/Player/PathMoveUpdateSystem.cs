using Friflo.Engine.ECS;
using UnityEngine;

[LevelScope]
[Order(1)]
public class PathMoveUpdateSystem : QueryUpdateSystem<PathFollowerComponent> {

    private readonly float turnSpeed = 400f;
    private Quaternion desiredRotation = Quaternion.identity;

    protected override void OnUpdate() {
        Query.ForEachEntity((ref PathFollowerComponent follower, Entity entity) => {
            ref var animatorComp = ref entity.GetComponent<AnimatorComponent>();
            if (follower.IsFinished) {
                CommandBuffer.RemoveComponent<PathFollowerComponent>(entity.Id);
                animatorComp.Value.CrossFade("Idle", 0.1f);
                return;
            }

            // Debug draw
            for (var i = follower.CurrentIndex; i < follower.Waypoints.Count - 1; i++) {
                Debug.DrawLine(follower.Waypoints[i], follower.Waypoints[i + 1]);
            }

            var transform = entity.GetTransform();
            ref var speedComp = ref entity.GetComponent<SpeedComponent>();

            var pos = transform.position;
            var target = follower.CurrentTarget;

            // Направление к текущей точке
            var toTarget = target - pos;
            toTarget.y = 0f;
            var dirToTarget = toTarget.normalized;


            var desiredRot = Quaternion.LookRotation(dirToTarget, Vector3.up);


            var maxDegrees = turnSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, maxDegrees);

            if (!follower.StartedMoving) {
                var angle = Quaternion.Angle(transform.rotation, desiredRot);
                if (angle > 2f) {
                    return;
                }

                animatorComp.Value.CrossFade("Walk", 0.1f);
                follower.StartedMoving = true;
            }

            var step = speedComp.Value * Time.deltaTime;
            var newPos = Vector3.MoveTowards(pos, target, step);
            transform.position = newPos;

            if (Vector3.Distance(transform.position, target) < 0.01f) {
                follower.CurrentIndex++;
            }
        });
    }

    protected void OnUpdated() {
        Query.ForEachEntity((ref PathFollowerComponent follower, Entity entity) => {
            if (follower.IsFinished) {
                CommandBuffer.RemoveComponent<PathFollowerComponent>(entity.Id);
                return;
            }

            for (var i = 0; i < follower.Waypoints.Count - 1; i++) {
                Debug.DrawLine(follower.Waypoints[i], follower.Waypoints[i + 1]);
            }

            var transform = entity.GetTransform();
            ref var speedComp = ref entity.GetComponent<SpeedComponent>();

            var target = follower.CurrentTarget;
            var step = speedComp.Value * Time.deltaTime;

            // 1. Двигаемся безопасно (MoveTowards не даст пролететь мимо)
            var newPos = Vector3.MoveTowards(transform.position, target, step);

            transform.position = newPos;

            if (Vector3.Distance(transform.position, target) < 0.01f) {
                follower.CurrentIndex++;
            }
        });
    }

}

// **********************
/*
if (follower.IsFinished) {
    CommandBuffer.RemoveComponent<PathFollowerComponent>(entity.Id);
    return;
}

// Debug линий маршрута
for (var i = 0; i < follower.Waypoints.Count - 1; i++) {
    Debug.DrawLine(follower.Waypoints[i], follower.Waypoints[i + 1]);
}

var transform = entity.GetTransform();
ref var speedComp = ref entity.GetComponent<SpeedComponent>();

// Скорость поворота (если нет компонента — дефолт)
var turnSpeed = 30f; // deg/sec дефолт

var target = follower.CurrentTarget;
var pos = transform.position;

// Направление к цели
var toTarget = target - pos;

// Если у тебя top-down и поворот только по Y — фиксируем Y
toTarget.y = 0f;

// Если цель почти совпадает — переходим к следующей точке
if (toTarget.sqrMagnitude < 0.0001f) {
    follower.CurrentIndex++;
    return;
}

var dir = toTarget.normalized;

// 1) ПОВОРОТ (всегда)
var desiredRot = Quaternion.LookRotation(dir, Vector3.up);

// Плавно поворачиваемся к desiredRot
var maxDegrees = turnSpeed * Time.deltaTime;
transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, maxDegrees);

// Проверяем, достаточно ли повернулись, чтобы начать движение
var angle = Quaternion.Angle(transform.rotation, desiredRot);
var canMove = angle <= 3f;

if (!canMove) {
    // СНАЧАЛА поворачиваемся — движение пока не начинаем
    return;
}

// 2) ДВИЖЕНИЕ (после поворота)
var step = speedComp.Value * Time.deltaTime;
var newPos = Vector3.MoveTowards(pos, target, step);
transform.position = newPos;

// 3) “Всегда смотреть куда идёт”
// На самом деле это уже выполняется, потому что каждый кадр dir пересчитывается и rotation подгоняется.
// Но если хочешь 100% соответствие "в сторону фактического движения", можно повернуть по velocity:
// var velocity = (newPos - pos); velocity.y = 0;
// if (velocity.sqrMagnitude > 0.000001f) {
//     var moveDir = velocity.normalized;
//     var moveRot = Quaternion.LookRotation(moveDir, Vector3.up);
//     transform.rotation = Quaternion.RotateTowards(transform.rotation, moveRot, maxDegrees);
// }

// Если дошли до текущей точки — двигаем индекс дальше
if (Vector3.Distance(transform.position, target) < 0.01f) {
    follower.CurrentIndex++;
}
*/