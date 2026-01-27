using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class TapResolveSystem : EntityListSystem<TapIntentComponent> {

    [Inject]
    private readonly ObjectPool<PooledCommandQueue> pool;
    [Inject]
    private readonly TapProviderResolver tapProviderResolver;

    protected override void ProcessEntity(ref TapIntentComponent tap, Entity player) {
        EntityDefinition def = null;
        if (!tap.Entity.IsNull) {
            def = tap.Entity.GetComponent<DefinitionComponent>().Value;
        }

        var endNode = AstarPath.active.GetNearest(tap.Position, NNConstraint.Walkable);
        var ctx = new TapContext
        {
            Actor = player,
            TargetPosition = tap.Position,
            TargetEntity = tap.Entity,
            EntityDef = def,
            MoveMode = tap.MoveMode,
            Node = endNode
        };

        var provider = tapProviderResolver.Resolve(ctx);
        if (provider != null) {
            var q = pool.Get();
            provider.Build(ctx, q);
            player.EmitSignal(new NewCommandPlanSignal { Value = q });
        }

        player.RemoveComponent<TapIntentComponent>();
    }

}