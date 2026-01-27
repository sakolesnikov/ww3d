using Friflo.Engine.ECS;
using UnityEngine;

/// <summary>
///     Represents the main, non-trigger 2D collider of the entity.
///     This collider is essential for physics interactions, including ground collision and gravity simulation.
/// </summary>
public struct Collider2DComponent : IComponent {

    public Collider2D Value;

}