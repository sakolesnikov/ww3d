using Friflo.Engine.ECS;

public struct LeftHandComponent : ILinkComponent {

    public Entity Entity;

    public Entity GetIndexedValue() => Entity;

}