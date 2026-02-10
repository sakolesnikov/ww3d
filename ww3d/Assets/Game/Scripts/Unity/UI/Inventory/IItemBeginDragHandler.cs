using Friflo.Engine.ECS;

/// <summary>
///     Synchronizes the state of an item entity when a drag operation begins from a specific container.
///     Implement this on container components (e.g., UserContainer, CraftContainer) to handle
///     logic when their child items are picked up.
/// </summary>
public interface IItemBeginDragHandler {

    /// <summary>
    ///     Called when the drag operation starts.
    /// </summary>
    /// <param name="entity">The entity of the item being dragged.</param>
    void OnItemBeginDrag(ref Entity entity);

}