using System.Collections.Generic;
using Friflo.Engine.ECS;
using UnityEngine;
using Transform = UnityEngine.Transform;

public class ToolPanel : MonoBehaviour, IEntityAware {

    [SerializeField]
    private List<Transform> items;
    [SerializeField]
    private List<Transform> crafts;
    private Entity entity;

    public void ShowInventory() {
        entity.EmitSignal(new OpenInventorySignal());
    }

    public void OnEntityReady(ref Entity entity) {
        this.entity = entity;
    }

    public void SwapHands() {
        entity.EmitSignal(new SwapHandsSignal());
    }

}