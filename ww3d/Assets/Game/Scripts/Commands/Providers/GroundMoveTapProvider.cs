using Pathfinding;

[LevelScope]
public class GroundMoveTapProvider : ITapProvider {

    public bool CanHandle(in TapContext ctx) {
        var endNode = AstarPath.active.GetNearest(ctx.TargetPosition, NNConstraint.Walkable);
        if (ctx.Actor.HasComponent<ActiveCommandComponent>()) {
            ref var activeCommandComp = ref ctx.Actor.GetComponent<ActiveCommandComponent>();
            var moveToCmd = (MoveToCmd)activeCommandComp.Value;
            return moveToCmd.Node.node.NodeIndex != endNode.node.NodeIndex || moveToCmd.MoveMode != ctx.MoveMode;
        }

        return true;
    }

    public void Build(in TapContext ctx, PooledCommandQueue queue) {
        queue.Enqueue(new MoveToCmd
        {
            Node = AstarPath.active.GetNearest(ctx.TargetPosition, NNConstraint.Walkable),
            MoveMode = ctx.MoveMode
        });
    }

    public int Order => 10;

}