using Friflo.Engine.ECS;
using UnityEngine.UI;

[LevelScope]
public class ActiveItemSystem : QueryUpdateSystem {

    private Entity player;
    private Entity toolPanel;

    protected override void OnAddStore(EntityStore store) {
        player = store.GetPlayer();
        toolPanel = store.GetToolPanel();
    }

    protected override void OnUpdate() {
        if (player.IsNull || toolPanel.IsNull) {
            return;
        }

        if (player.ChildCount == 0) {
            ref var imageComp = ref toolPanel.GetComponent<ImageComponent>();
            ref var activeItemDefComp = ref toolPanel.GetComponent<ItemDefinitionComponent>();
            if (activeItemDefComp.Value != null) {
                activeItemDefComp.Value = null;
                imageComp.Value.sprite = null;
                SetAlpha(imageComp.Value, 0);
            }

            return;
        }

        if (player.ChildCount == 1) {
            var lootEntity = player.ChildEntities[0];
            SetActiveItem(ref lootEntity);
        }

        if (player.ChildCount > 1) {
            ref var activeItemIndexComp = ref player.GetComponent<ActiveItemComponent>();
            var lootEntity = player.ChildEntities[activeItemIndexComp.Index];
            SetActiveItem(ref lootEntity);
        }
    }

    private void SetActiveItem(ref Entity lootEntity) {
        ref var activeItemDefComp = ref toolPanel.GetComponent<ItemDefinitionComponent>();
        ref var lootDefComp = ref lootEntity.GetComponent<DefinitionComponent>();
        if (activeItemDefComp.Value == null || lootDefComp.Value.GetType() != activeItemDefComp.Value.GetType()) {
            activeItemDefComp.Value = lootDefComp.Value;
            ref var imageComp = ref toolPanel.GetComponent<ImageComponent>();
            imageComp.Value.sprite = lootDefComp.GetValue<LootDef>().Sprite;
            SetAlpha(imageComp.Value, 1);
        }
    }

    private void SetAlpha(Image sprite, float alpha) {
        var color = sprite.color;
        color.a = alpha;
        sprite.color = color;
    }

}