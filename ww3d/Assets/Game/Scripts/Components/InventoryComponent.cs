using Friflo.Engine.ECS;
using Transform = UnityEngine.Transform;

public struct InventoryComponent : IComponent {

    public Transform PlayerContent;
    public Transform LeftHandContent;
    public Transform RightHandContent;

}