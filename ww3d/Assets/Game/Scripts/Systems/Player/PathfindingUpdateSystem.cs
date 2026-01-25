using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;
using VContainer;

[LevelScope]
public class PathfindingUpdateSystem : QueryUpdateSystem<MoveIntentComponent> {

    [Inject]
    private readonly FunnelModifier funnelModifier;
    [Inject]
    private readonly SimpleSmoothModifier simpleSmoothModifier;

    protected override void OnUpdate() {
        Query.ForEachEntity((ref MoveIntentComponent intent, Entity entity) => {
            CommandBuffer.RemoveComponent<MoveIntentComponent>(entity.Id);

            ref var transformComp = ref entity.GetComponent<TransformComponent>();
            var transform = transformComp.Value;

            var endNode = AstarPath.active.GetNearest(intent.Target, NNConstraint.Walkable);
            if (entity.HasComponent<PathFollowerComponent>()) {
                ref var pathFollowerComp = ref entity.GetComponent<PathFollowerComponent>();
                if (pathFollowerComp.TargetNode.node.NodeIndex == endNode.node.NodeIndex && pathFollowerComp.MoveMode != intent.MoveMode) {
                    pathFollowerComp.MoveMode = intent.MoveMode;
                    Debug.Log("change move mode");
                    return;
                }
            }

            var path = ABPath.Construct(transform.position, endNode.position);

            AstarPath.StartPath(path);
            path.BlockUntilCalculated();

            if (path.error) {
                return;
            }

            funnelModifier.Apply(path);
            simpleSmoothModifier.Apply(path);
            CommandBuffer.AddComponent(entity.Id,
                new PathFollowerComponent { Waypoints = path.vectorPath, CurrentIndex = 1, MoveMode = intent.MoveMode, TargetNode = endNode });
        });
    }

}