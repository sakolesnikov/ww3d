using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;

public abstract class QueryUpdateSystem : QuerySystem, IUpdateSystem { }

public abstract class QueryUpdateSystem<T> : QuerySystem<T>, IUpdateSystem where T : struct, IComponent { }

public abstract class EntityListSystem<T> : QueryUpdateSystem<T> where T : struct, IComponent {

    private readonly EntityList entityList = new();

    protected virtual bool CanProcess() => true;

    protected abstract void ProcessEntity(ref T component, Entity entity);

    protected override void OnUpdate() {
        if (!CanProcess()) {
            return;
        }

        Query.Entities.ToEntityList(entityList);
        foreach (var entity in entityList) {
            ProcessEntity(ref entity.GetComponent<T>(), entity);
        }
    }

}

public abstract class EntityListSystem<T1, T2> : QueryUpdateSystem<T1, T2> where T1 : struct, IComponent where T2 : struct, IComponent {

    private readonly EntityList entityList = new();

    protected virtual bool CanProcess() => true;

    protected abstract void ProcessEntity(ref T1 comp1, ref T2 comp2, Entity entity);

    protected override void OnUpdate() {
        if (!CanProcess()) {
            return;
        }

        Query.Entities.ToEntityList(entityList);
        foreach (var entity in entityList) {
            ProcessEntity(ref entity.GetComponent<T1>(), ref entity.GetComponent<T2>(), entity);
        }
    }

}

public abstract class EntityListSystem<T1, T2, T3> : QueryUpdateSystem<T1, T2, T3>
    where T1 : struct, IComponent
    where T2 : struct, IComponent
    where T3 : struct, IComponent {

    private readonly EntityList entityList = new();

    protected virtual bool CanProcess() => true;

    protected abstract void ProcessEntity(ref T1 comp1, ref T2 comp2, ref T3 comp3, Entity entity);

    protected override void OnUpdate() {
        if (!CanProcess()) {
            return;
        }

        Query.Entities.ToEntityList(entityList);
        foreach (var entity in entityList) {
            ProcessEntity(ref entity.GetComponent<T1>(), ref entity.GetComponent<T2>(), ref entity.GetComponent<T3>(), entity);
        }
    }

}

public abstract class QueryUpdateSystem<T1, T2> : QuerySystem<T1, T2>, IUpdateSystem
    where T1 : struct, IComponent where T2 : struct, IComponent { }

public abstract class QueryUpdateSystem<T1, T2, T3> : QuerySystem<T1, T2, T3>, IUpdateSystem
    where T1 : struct, IComponent where T2 : struct, IComponent where T3 : struct, IComponent { }