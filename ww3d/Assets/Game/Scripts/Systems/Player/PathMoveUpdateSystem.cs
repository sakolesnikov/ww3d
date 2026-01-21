using Friflo.Engine.ECS;
using UnityEngine;

[LevelScope]
[Order(1)]
public class PathMoveUpdateSystem : QueryUpdateSystem<PathFollowerComponent> {

    protected override void OnUpdate() {
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