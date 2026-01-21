using System;
using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class LootableTapCommandBuilder : ICommandBuilder {

    [Inject]
    private readonly ObjectPool<PooledCommandQueue> pool;
    [Inject]
    private readonly EntityStore world;

    public PooledCommandQueue Build() {
        var cursor = world.GetCursor();
        var pos = cursor.GetComponent<CursorComponent>().Position;
        var pooledQueue = pool.Get();
        pooledQueue.Enqueue(new MoveToCmd { Target = pos });
        pooledQueue.Enqueue(new OpenInventoryCmd());
        pooledQueue.Enqueue(new SetDefaultCursorCmd(world.GetCursor()));
        return pooledQueue;
    }

    public Type GetEntityType() => typeof(LootableEntityDef);

}