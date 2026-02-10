using Friflo.Engine.ECS;

/// <summary>
///     Represents a visual link between an inventory container and its UI representation.
///     Used to track which entities are currently being displayed in the interface,
///     allowing for efficient cleanup and returning items to the pool when the window is closed.
/// </summary>
public struct ShowsRelation : ILinkRelation {

    /// <summary>The entity currently being displayed in the UI.</summary>
    public Entity Entity;

    public Entity GetRelationKey() => Entity;

}