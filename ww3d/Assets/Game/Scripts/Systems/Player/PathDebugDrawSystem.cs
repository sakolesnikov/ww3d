using Friflo.Engine.ECS;
using UnityEngine;

#if UNITY_EDITOR
[LevelScope]
public class PathDebugDrawSystem : QueryUpdateSystem<PathFollowerComponent> {

    protected override void OnUpdate() {
        Query.ForEachEntity((ref PathFollowerComponent follower, Entity entity) => {
            if (follower.IsFinished) {
                return;
            }

            for (var i = follower.CurrentIndex; i < follower.Waypoints.Count - 1; i++) {
                Debug.DrawLine(follower.Waypoints[i], follower.Waypoints[i + 1]);
            }
        });
    }

}
#endif