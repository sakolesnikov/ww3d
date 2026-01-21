using System;
using Friflo.Engine.ECS;

public struct PooledObjectComponent : IComponent {

    public IDisposable Value;

}