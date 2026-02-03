using Friflo.Engine.ECS;

public struct LeftHandComponent : ILinkComponent, IBodyPart {

    public Entity Entity;

    public Entity GetIndexedValue() => Entity;

    public Entity Target {
        get => Entity;
        set => Entity = value;
    }

}