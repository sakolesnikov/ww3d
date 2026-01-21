using Friflo.Engine.ECS;

public struct ShowsRelation : ILinkRelation {

    public Entity Entity;

    public Entity GetRelationKey() => Entity;

}