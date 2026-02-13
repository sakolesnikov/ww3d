using System;
using Friflo.Engine.ECS;

[LevelScope]
public class FlashlightChargedInit : IEntityInitialization {

    public void Initialize(Entity entity) {
        entity.AddComponent(new LifecycleSignalsComponent
        {
            CraftedSignal = static world => world.EmitSignal(new InventoryItemCraftedSignal<FlashlightChargedDef>()),
            InventoryAddedSignal = static world => world.EmitSignal(new InventoryItemAddedSignal<FlashlightChargedDef>()),
            InventoryRemovedSignal = static world => world.EmitSignal(new InventoryItemRemovedSignal<FlashlightChargedDef>())
        });
        entity.AddComponent(new TooltipComponent { Key = entity.GetComponent<DefinitionComponent>().Value.EntityName });
    }

    public Type getType() => typeof(FlashlightChargedDef);

}

public struct LifecycleSignalsComponent : IComponent {

    public Action<EntityStore> CraftedSignal;
    public Action<EntityStore> InventoryAddedSignal;
    public Action<EntityStore> InventoryRemovedSignal;

}

public struct InventoryItemCraftedSignal<T> where T : LootDef { }

public struct InventoryItemAddedSignal<T> where T : LootDef { }

public struct InventoryItemRemovedSignal<T> where T : LootDef { }