using Friflo.Engine.ECS;
using UnityEngine;

/// <summary>
///     Stores the pre-calculated <see cref="UnityEngine.Bounds" /> (AABB) of an Entity.
/// </summary>
/// <remarks>
///     This component is strictly intended for **static, non-moving Entities** /// (e.g., environment structures, terrain
///     chunks) whose bounds do not change at runtime.
///     The component ensures that the bounding box is computed **only once** during initialization
///     or level loading, allowing other systems to quickly access the AABB without having to
///     calculate it every frame.
///     It should **not** be used for dynamic or moving objects, as their bounds
///     would become instantly outdated, leading to inaccurate collision or frustum culling.
/// </remarks>
public struct BoundsComponent : IComponent {

    /// <summary>
    ///     The pre-calculated axis-aligned bounding box (AABB).
    /// </summary>
    public Bounds Value;

}