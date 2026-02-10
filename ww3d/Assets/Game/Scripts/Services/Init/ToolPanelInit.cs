using System;
using Friflo.Engine.ECS;

[LevelScope]
public class ToolPanelInit : IEntityInitialization {

    public Type getType() => typeof(ToolPanelDef);

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var toolPanel = go.GetComponent<ToolPanel>();
        entity.AddComponent(new ToolPanelComponent { Items = toolPanel.Items, Crafts = toolPanel.Crafts });
    }

}