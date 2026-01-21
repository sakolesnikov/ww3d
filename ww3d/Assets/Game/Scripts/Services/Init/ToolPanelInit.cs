using System;
using Friflo.Engine.ECS;
using UnityEngine.UI;

[LevelScope]
public class ToolPanelInit : IEntityInitialization {

    public Type getType() => typeof(ToolPanelDef);

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var toolPanel = go.GetComponent<ToolPanel>();
        entity.AddComponent(new ImageComponent { Value = toolPanel.ActiveItem.GetComponent<Image>() });
        entity.AddComponent(new ItemDefinitionComponent());
    }

}