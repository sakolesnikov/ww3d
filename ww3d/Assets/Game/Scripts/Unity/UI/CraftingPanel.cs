using Friflo.Engine.ECS;
using UnityEngine;
using Transform = UnityEngine.Transform;

public class CraftingPanel : MonoBehaviour, IEntityAware {

    [SerializeField]
    private Transform content;
    private Entity entity;
    public Transform Content => content;

    public void OnEntityReady(ref Entity entity) {
        this.entity = entity;
    }

    public void Craft() {
        entity.EmitSignal(new CraftSignal());
    }

}