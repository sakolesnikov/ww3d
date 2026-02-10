using System;
using Friflo.Engine.ECS;

[LevelScope]
public class ToolPanelInit : IEntityInitialization {

    public Type getType() => typeof(ToolPanelDef);

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var toolPanel = go.GetComponent<ToolPanel>();
        // entity.AddComponent(new ImageComponent { Value = toolPanel.ActiveItem.GetComponent<Image>() });
        entity.AddComponent(new ItemDefinitionComponent());
        // entity.AddComponent(new MessageComponent { Value = toolPanel.Messages });
        // entity.AddComponent(new ScrollRectComponent { Value = go.GetComponentInChildren<ScrollRect>() });
    }

}