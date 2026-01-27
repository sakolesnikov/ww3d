using Pathfinding;
using UnityEngine;

[LevelScope]
public class LootableTapProvider : ITapProvider {

    public bool CanHandle(in TapContext ctx) => ctx.HasTarget && ctx.TargetEntity.HasComponent<LootComponent>();

    public void Build(in TapContext ctx, PooledCommandQueue queue) {
        var node = AstarPath.active.GetNearest(ctx.TargetPosition, NNConstraint.Walkable);
        Debug.Log($"nodePosition {node.position}, ctx.TargetPosition {ctx.TargetEntity}");
        queue.Enqueue(new MoveToCmd
        {
            Node = AstarPath.active.GetNearest(ctx.TargetPosition, NNConstraint.Walkable),
            MoveMode = ctx.MoveMode,
            Target = ctx.TargetPosition
        });
        queue.Enqueue(new OpenInventoryCmd());
    }

    public int Order => 1;

}