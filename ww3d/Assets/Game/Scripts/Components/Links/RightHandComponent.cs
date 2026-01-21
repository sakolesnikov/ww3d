using Friflo.Engine.ECS;

public struct RightHandComponent : ILinkComponent {

    public Entity Entity;

    public Entity GetIndexedValue() => Entity;

}