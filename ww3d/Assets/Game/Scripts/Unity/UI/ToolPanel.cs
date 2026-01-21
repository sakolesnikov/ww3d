using Friflo.Engine.ECS;
using UnityEngine;

public class ToolPanel : MonoBehaviour, IEntityAware {

    [SerializeField]
    private GameObject activeItem;
    public GameObject ActiveItem => activeItem;
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