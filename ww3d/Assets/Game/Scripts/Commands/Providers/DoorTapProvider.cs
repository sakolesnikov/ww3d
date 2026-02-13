using Pathfinding;

[LevelScope]
public class DoorTapProvider : ITapProvider {

    public bool CanHandle(in TapContext ctx) => ctx.HasTarget && ctx.EntityDef.GetType() == typeof(DoorDef);

    public void Build(in TapContext ctx, PooledCommandQueue queue) {
        var targetEntityCollider = ctx.TargetEntity.GetComponent<ColliderComponent>().Value;
        var closestOnEntity = targetEntityCollider.ClosestPoint(ctx.Actor.GetTransform().position);
        var node = AstarPath.active.GetNearest(closestOnEntity, NNConstraint.Walkable);
        queue.Enqueue(new MoveToCmd { Node = node, MoveMode = ctx.MoveMode, TargetEntity = ctx.TargetEntity });
        if (ctx.TargetEntity.Tags.Has<OpenedTag>()) {
            queue.Enqueue(new CloseDoorCmd { Door = ctx.TargetEntity });
        } else {
            queue.Enqueue(new OpenDoorCmd { Door = ctx.TargetEntity });
        }
    }

    public int Order => 1;

}