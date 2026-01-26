using Friflo.Engine.ECS;
using UnityEngine;

[Order(10)]
[LevelScope]
public class PathMoveSystem : QueryUpdateSystem<PathFollowerComponent> {

    protected override void OnUpdate() {
        Query.ForEachEntity((ref PathFollowerComponent follower, Entity entity) => {
            if (follower.IsFinished) {
                CommandBuffer.RemoveComponent<PathFollowerComponent>(entity.Id);
                return;
            }

            var transform = entity.GetTransform();
            ref var speedComp = ref entity.GetComponent<SpeedComponent>();

            var pos = transform.position;
            var target = follower.CurrentTarget;

            var toTarget = target - pos;
            toTarget.y = 0f;
            var dirToTarget = toTarget.normalized;

            var desiredRot = Quaternion.LookRotation(dirToTarget, Vector3.up);


            var maxDegrees = speedComp.Rotation * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRot, maxDegrees);

            var step = speedComp.Value * Time.deltaTime;
            var newPos = Vector3.MoveTowards(pos, target, step);
            transform.position = newPos;

            if (Vector3.Distance(transform.position, target) < 0.01f) {
                follower.CurrentIndex++;
            }
        });
    }

}