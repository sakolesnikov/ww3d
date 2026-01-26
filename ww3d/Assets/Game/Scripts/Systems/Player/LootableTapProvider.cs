using Pathfinding;
using UnityEngine;

[LevelScope]
public class LootableTapProvider : ITapProvider {

    public bool CanHandle(in TapContext ctx) => !ctx.TargetEntity.IsNull && ctx.TargetEntity.HasComponent<LootComponent>();

    public void Build(in TapContext ctx, PooledCommandQueue queue) {
        var node = AstarPath.active.GetNearest(ctx.TargetPosition, NNConstraint.Walkable);
        Debug.Log($"nodePosition {node.position}, ctx.TargetPosition {node.position}");
        queue.Enqueue(new MoveToCmd
        {
            endNode = AstarPath.active.GetNearest(ctx.TargetPosition, NNConstraint.Walkable),
            MoveMode = ctx.MoveMode,
            Target = ctx.TargetPosition
        });
        queue.Enqueue(new OpenInventoryCmd());
    }

    public int Order => 1;

}