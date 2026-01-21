using Friflo.Engine.ECS;
using UnityEngine;
using Transform = UnityEngine.Transform;

public class InventoryWindow : MonoBehaviour, IEntityAware {

    private Entity entity;
    [SerializeField]
    private Transform playerContent;
    [SerializeField]
    private Transform leftHandContent;
    [SerializeField]
    private Transform rightHandContent;
    public Transform PlayerContent => playerContent;
    public Transform LeftHandContent => leftHandContent;
    public Transform RightHandContent => rightHandContent;

    public void Close() {
        entity.EmitSignal(new CloseInventorySignal());
    }

    public void OnEntityReady(ref Entity entity) {
        this.entity = entity;
    }

}