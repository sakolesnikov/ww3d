using Friflo.Engine.ECS;

/// <summary>
///     Represents logical containment relationship.
///     Used to indicate that a parent entity (e.g., a table) contains a specific child entity (e.g., a gun).
/// </summary>
public struct ContainsRelation : ILinkRelation {

    /// <summary>The child entity contained within the parent.</summary>
    public Entity Entity;

    public Entity GetRelationKey() => Entity;

}