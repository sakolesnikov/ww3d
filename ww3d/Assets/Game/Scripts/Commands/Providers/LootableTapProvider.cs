using Pathfinding;

[LevelScope]
public class LootableTapProvider : ITapProvider {

    public bool CanHandle(in TapContext ctx) => ctx.HasTarget && ctx.TargetEntity.HasComponent<LootComponent>();

    public void Build(in TapContext ctx, PooledCommandQueue queue) {
        var targetEntityCollider = ctx.TargetEntity.GetComponent<ColliderComponent>().Value;
        var closestOnEntity = targetEntityCollider.ClosestPoint(ctx.Actor.GetTransform().position);

        var node = AstarPath.active.GetNearest(closestOnEntity, NNConstraint.Walkable);

        queue.Enqueue(new MoveToCmd
        {
            Node = node,
            MoveMode = ctx.MoveMode,
            TargetEntity = ctx.TargetEntity
        });
        queue.Enqueue(new OpenExchangeCmd { Target = ctx.TargetEntity });
    }

    public int Order => 1;

}