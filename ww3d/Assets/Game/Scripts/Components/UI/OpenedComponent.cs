using Friflo.Engine.ECS;

/// <summary>
///     Indicates which entity is currently loaded in the exchange inventory for object swapping.
/// </summary>
public struct OpenedComponent : IComponent {

    /// <summary>
    ///     The entity currently active in the exchange process.
    /// </summary>
    public Entity Value;

}