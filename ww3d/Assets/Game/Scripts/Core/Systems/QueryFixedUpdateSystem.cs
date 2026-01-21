using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;

public abstract class QueryFixedUpdateSystem : QuerySystem, IFixedUpdateSystem { }

public abstract class QueryFixedUpdateSystem<T> : QuerySystem<T>, IFixedUpdateSystem where T : struct, IComponent { }