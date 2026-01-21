using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;

public abstract class QueryUpdateSystem : QuerySystem, IUpdateSystem { }

public abstract class QueryUpdateSystem<T> : QuerySystem<T>, IUpdateSystem where T : struct, IComponent { }

public abstract class QueryUpdateSystem<T1, T2> : QuerySystem<T1, T2>, IUpdateSystem
    where T1 : struct, IComponent where T2 : struct, IComponent { }

public abstract class QueryUpdateSystem<T1, T2, T3> : QuerySystem<T1, T2, T3>, IUpdateSystem
    where T1 : struct, IComponent where T2 : struct, IComponent where T3 : struct, IComponent { }