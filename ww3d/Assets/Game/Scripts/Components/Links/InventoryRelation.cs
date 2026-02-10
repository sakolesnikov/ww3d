using Friflo.Engine.ECS;

/// <summary>
///     Represents a relationship indicating that an entity is currently owned by the player
///     and resides within a standard inventory slot.
///     Similar to <see cref="CraftRelation" />, which denotes player ownership for items
///     placed specifically in crafting slots.
/// </summary>
public struct InventoryRelation : ILinkRelation {

    /// <summary>The entity stored in the inventory/crafting slot.</summary>
    public Entity Entity;

    public Entity GetRelationKey() => Entity;

}