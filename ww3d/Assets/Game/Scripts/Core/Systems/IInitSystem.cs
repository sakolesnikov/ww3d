using Friflo.Engine.ECS;

/// <summary>
///     Defines a contract for systems that require initialization logic
///     to be executed once when the ECS world or the application module starts up.
/// </summary>
/// <remarks>
///     Systems implementing this interface typically use the <see cref="Init" /> method
///     to perform initial setup tasks, such as subscribing to global events (e.g., entity creation/deletion),
///     fetching initial data.
/// </remarks>
public interface IInitSystem {

    /// <summary>
    ///     Executes the system's one-time initialization logic.
    /// </summary>
    /// <param name="world">The main EntityStore (World) instance, providing access to entities and global events.</param>
    public void Init(EntityStore world);

}