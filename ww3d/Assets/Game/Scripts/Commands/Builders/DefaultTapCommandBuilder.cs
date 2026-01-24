using System;
using Friflo.Engine.ECS;
using UnityEngine.Pool;
using VContainer;

[LevelScope]
public class DefaultTapCommandBuilder : ICommandBuilder {

    [Inject]
    private readonly ObjectPool<PooledCommandQueue> pool;
    [Inject]
    private readonly EntityStore world;

    public PooledCommandQueue Build() {
        var cursor = world.GetCursor();
        var pooledQueue = pool.Get();
        if (cursor.IsNull) {
            return pooledQueue; 
        }
        var pos = cursor.GetComponent<CursorComponent>().Position;
        pooledQueue.Enqueue(new MoveToCmd { Target = pos });
        return pooledQueue;
    }

    public Type GetEntityType() => typeof(NoGenericEntityDef);

}