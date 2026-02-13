using Friflo.Engine.ECS;
using UnityEngine;

public static class EntityStoreExtensions {

    public static Canvas GetCanvas(this EntityStore world) {
        var entity = world.GetUniqueEntity(CanvasDef.Name);
        ref var canvasComp = ref entity.GetComponent<CanvasComponent>();
        return canvasComp.Value;
    }

    public static Entity GetTooltip(this EntityStore world) => world.GetUniqueEntity(TooltipDef.Name);

    public static void EmitSignal<TEvent>(this EntityStore world, in TEvent ev) where TEvent : struct =>
        world.GetSignalBus().EmitSignal(ev);

    public static Entity GetRecipeRegistry(this EntityStore world) => world.GetUniqueEntitySafe(RecipeRegistryDef.Name);

    public static Entity GetSignalBus(this EntityStore world) => world.GetUniqueEntitySafe(SignalBusDef.Name);

    public static Entity GetCamera(this EntityStore world) => world.GetUniqueEntitySafe(CameraDef.Name);

    public static Entity GetPlayer(this EntityStore world) => world.GetUniqueEntitySafe(PlayerDef.Name);

    public static Entity GetExchange(this EntityStore world) => world.GetUniqueEntitySafe(ExchangeDef.Name);

    public static Entity GetInventoryWnd(this EntityStore world) => world.GetUniqueEntitySafe(InventoryWindowDef.Name);

    public static Entity GetToolPanel(this EntityStore world) => world.GetUniqueEntitySafe(ToolPanelDef.Name);

    public static PlayerDef GetPlayerDef(this EntityStore world) {
        if (world.GetUniqueEntitySafe(PlayerDef.Name) is { IsNull: false } p) {
            p.GetComponent<DefinitionComponent>().GetValue<PlayerDef>();
        }

        return null;
    }

    public static Entity GetCursor(this EntityStore world) => world.GetUniqueEntitySafe(CursorDef.Name);

    private static Entity GetUniqueEntitySafe(this EntityStore world, string entityName) {
        var index = world.ComponentIndex<UniqueEntity, string>();
        var entities = index[entityName];
        if (entities.Count == 0) {
            // Debug.Log("Entity " + entityName + " not found");
            return default;
        }

        return entities[0];
    }

}