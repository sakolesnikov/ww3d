using Friflo.Engine.ECS;

/// <summary>
///     Specifies the sorting order for entities during the initialization phase.
///     This component is utilized by <see cref="IEntityInitialization" /> to determine
///     the setup sequence of entities.
/// </summary>
public struct OrderComponent : IComponent {

    /// <summary>
    ///     The numeric priority value. Lower values typically indicate higher priority
    ///     or earlier processing.
    /// </summary>
    public int Value;

}