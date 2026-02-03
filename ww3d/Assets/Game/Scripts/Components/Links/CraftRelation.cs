using Friflo.Engine.ECS;

public struct CraftRelation : ILinkRelation {

    public Entity Entity;

    public Entity GetRelationKey() => Entity;

}