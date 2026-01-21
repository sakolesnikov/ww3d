using Friflo.Engine.ECS;
using Pathfinding;

[LevelScope]
public class PathfindingUpdateSystem : QueryUpdateSystem<MoveIntentComponent> {

    private PlayerDef playerDef;

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
            pos.z = 0f;

            var path = ABPath.Construct(transform.position, pos);

            AstarPath.StartPath(path);
            path.BlockUntilCalculated();

            if (path.error) {
                return;
            }

            // foreach (var point in path.vectorPath) {
            // Object.Instantiate(playerDef.CellMarker, point, Quaternion.identity);
            // }

            CommandBuffer.AddComponent(entity.Id, new PathFollowerComponent { Waypoints = path.vectorPath, CurrentIndex = 1 });
        });
    }

}