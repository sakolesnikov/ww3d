using Friflo.Engine.ECS;
using Transform = UnityEngine.Transform;

/// <summary>
///     Stores the original parent transform of an entity.
/// </summary>
/// <remarks>
///     This component is primarily used during drag-and-drop operations:
///     <list type="bullet">
///         <item>Before starting a drag (e.g., moving an item between inventories), the entity is unparented.</item>
///         <item>
///             If the entity is dropped outside of a valid container, it uses this stored reference to return to its
///             original position.
///         </item>
///     </list>
/// </remarks>
public struct ParentTransformComponent : IComponent {

    public Transform Value;

}