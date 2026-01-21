using Friflo.Engine.ECS;

public struct ContainsRelation : ILinkRelation {

    public Entity Entity;

    public Entity GetRelationKey() => Entity;

}