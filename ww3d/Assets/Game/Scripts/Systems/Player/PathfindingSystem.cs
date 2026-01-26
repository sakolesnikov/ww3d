using Friflo.Engine.ECS;
using Pathfinding;
using VContainer;

[Order(0)]
[LevelScope]
public class PathfindingSystem : QueryUpdateSystem<MoveIntentComponent> {

    [Inject]
    private readonly FunnelModifier funnelModifier;
    [Inject]
    private readonly SimpleSmoothModifier simpleSmoothModifier;

    protected override void OnUpdate() {
        Query.ForEachEntity((ref MoveIntentComponent intent, Entity entity) => {
            CommandBuffer.RemoveComponent<MoveIntentComponent>(entity.Id);

            ref var transformComp = ref entity.GetComponent<TransformComponent>();
            var transform = transformComp.Value;

            if (entity.HasComponent<PathFollowerComponent>()) {
                ref var pathFollowerComp = ref entity.GetComponent<PathFollowerComponent>();
                if (pathFollowerComp.TargetNode.node.NodeIndex == intent.endNode.node.NodeIndex && pathFollowerComp.MoveMode != intent.MoveMode) {
                    pathFollowerComp.MoveMode = intent.MoveMode;
                    return;
                }
            }

            var path = ABPath.Construct(transform.position, intent.endNode.position);

            AstarPath.StartPath(path);
            path.BlockUntilCalculated();

            if (path.error) {
                return;
            }

            if (path.vectorPath.Count <= 1) {
                return;
            }

            funnelModifier.Apply(path);
            simpleSmoothModifier.Apply(path);
            CommandBuffer.AddComponent(entity.Id,
                new PathFollowerComponent { Waypoints = path.vectorPath, CurrentIndex = 1, MoveMode = intent.MoveMode, TargetNode = intent.endNode });
        });
    }

}