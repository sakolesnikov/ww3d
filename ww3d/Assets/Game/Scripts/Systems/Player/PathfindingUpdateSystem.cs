using Friflo.Engine.ECS;
using Pathfinding;
using VContainer;

[LevelScope]
public class PathfindingUpdateSystem : QueryUpdateSystem<MoveIntentComponent> {

    private PlayerDef playerDef;
    [Inject]
    private readonly FunnelModifier funnelModifier;
    [Inject]
    private readonly SimpleSmoothModifier simpleSmoothModifier;

    protected override void OnAddStore(EntityStore store) {
        playerDef = store.GetPlayerDef();
    }

    protected override void OnUpdate() {
        Query.ForEachEntity((ref MoveIntentComponent intent, Entity entity) => {
            CommandBuffer.RemoveComponent<MoveIntentComponent>(entity.Id);

            ref var transformComp = ref entity.GetComponent<TransformComponent>();
            var transform = transformComp.Value;

            var endNod = AstarPath.active.GetNearest(intent.Target, NNConstraint.Walkable);
            var pos = endNod.position;
            // pos.z = 0f;

            var path = ABPath.Construct(transform.position, pos);

            AstarPath.StartPath(path);
            path.BlockUntilCalculated();

            if (path.error) {
                return;
            }

            funnelModifier.Apply(path);
            simpleSmoothModifier.Apply(path);
            CommandBuffer.AddComponent(entity.Id, new PathFollowerComponent { Waypoints = path.vectorPath, CurrentIndex = 1 });
        });
    }

}