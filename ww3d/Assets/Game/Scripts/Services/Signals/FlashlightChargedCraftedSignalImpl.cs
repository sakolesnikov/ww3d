using Friflo.Engine.ECS;
using UnityEngine;

[LevelScope]
public class FlashlightChargedCraftedSignalImpl : GenericSignal<ItemCraftedSignal> {

    protected override void Signal(Signal<ItemCraftedSignal> signal) {
        Debug.Log("crafted");
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(FlashlightChargedDef);

}