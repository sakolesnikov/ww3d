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
    public List<Transform> Items => items;
    public List<Transform> Crafts => crafts;

    public void ShowInventory() {
        entity.EmitSignal(new OpenInventorySignal());
    }

    public void OnEntityReady(ref Entity entity) {
        this.entity = entity;
    }

    public void Craft() {
        entity.EmitSignal(new CraftSignal());
    }

}