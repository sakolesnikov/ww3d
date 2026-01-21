using System;
using Friflo.Engine.ECS;

[LevelScope]
public class InventoryInit : IEntityInitialization {

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var invWnd = go.GetComponent<InventoryWindow>();
        entity.AddComponent(new InventoryComponent
            { PlayerContent = invWnd.PlayerContent, LeftHandContent = invWnd.LeftHandContent, RightHandContent = invWnd.RightHandContent });
    }

    public Type getType() => typeof(InventoryWindowDef);

}