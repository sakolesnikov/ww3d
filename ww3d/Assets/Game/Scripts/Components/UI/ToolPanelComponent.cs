using System.Collections.Generic;
using Friflo.Engine.ECS;
using Transform = UnityEngine.Transform;

public struct ToolPanelComponent : IComponent {

    public List<Transform> Items;
    public List<Transform> Crafts;

    public Transform FindEmptySlot(List<Transform> slots) {
        if (slots == null) {
            return null;
        }

        foreach (var slot in slots) {
            if (slot.childCount == 0) {
                return slot;
            }
        }

        return null;
    }

    public Transform FindEmptyItemSlot() => FindEmptySlot(Items);

    public Transform FindEmptyCraftSlot() => FindEmptySlot(Crafts);

    public Transform FindFirstEmptySlot() {
        var slot = FindEmptySlot(Items);
        if (slot == null) {
            return FindEmptySlot(Crafts);
        }

        return slot;
    }

    public bool TryFindEmptySlot(out Transform slot, out ILinkRelation relation) {
        slot = FindEmptySlot(Items);
        if (slot != null) {
            relation = new InventoryRelation();
            return true;
        }

        slot = FindEmptySlot(Crafts);
        if (slot != null) {
            relation = new CraftRelation();
            return true;
        }

        relation = null;
        return false;
    }

}