using Friflo.Engine.ECS;
using UnityEngine;
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
            // ref var activeItemDefComp = ref toolPanel.GetComponent<ItemDefinitionComponent>();
            // ref var lootDefComp = ref lootEntity.GetComponent<DefinitionComponent>();
            // if (activeItemDefComp.Value == null || lootDefComp.Value.GetType() != activeItemDefComp.Value.GetType()) {
            // ref var imageComp = ref toolPanel.GetComponent<ImageComponent>();
            // imageComp.Value.sprite = lootDefComp.GetValue<LootDef>().Sprite;
            // SetAlpha(imageComp.Value, 1);
            // }
        }

        if (player.ChildCount > 1) {
            ref var activeItemIndexComp = ref player.GetComponent<ActiveItemComponent>();
            var lootEntity = player.ChildEntities[activeItemIndexComp.Index];
            SetActiveItem(ref lootEntity);
        }
        // if (player.HasComponent<LeftHandComponent>()) {
        //     ref var leftHandComp = ref player.GetComponent<LeftHandComponent>();
        //     ref var lootDefComp = ref leftHandComp.Entity.GetComponent<DefinitionComponent>();
        //     ref var imageComp = ref toolPanel.GetComponent<ImageComponent>();
        //     ref var activeItemDefComp = ref toolPanel.GetComponent<ItemDefinitionComponent>();
        //     if (activeItemDefComp.Value == null || lootDefComp.Value.GetType() != activeItemDefComp.Value.GetType()) {
        //         imageComp.Value.sprite = lootDefComp.GetValue<LootDef>().Sprite;
        //         SetAlpha(imageComp.Value, 1);
        //     }
        //
        //     return;
        // } else {
        //     ref var imageComp = ref toolPanel.GetComponent<ImageComponent>();
        //     ref var activeItemDefComp = ref toolPanel.GetComponent<ItemDefinitionComponent>();
        //     if (!activeItemDefComp.Value) {
        //         activeItemDefComp.Value = null;
        //         imageComp.Value.sprite = null;
        //         SetAlpha(imageComp.Value, 0);
        //     }
        // }
        //
        // if (player.HasComponent<RightHandComponent>()) {
        //     ref var rightLeftHandComp = ref player.GetComponent<RightHandComponent>();
        //     ref var lootDefComp = ref rightLeftHandComp.Entity.GetComponent<DefinitionComponent>();
        //     ref var imageComp = ref toolPanel.GetComponent<ImageComponent>();
        //     ref var activeItemDefComp = ref toolPanel.GetComponent<ItemDefinitionComponent>();
        //     if (activeItemDefComp.Value == null || lootDefComp.Value.GetType() != activeItemDefComp.Value.GetType()) {
        //         imageComp.Value.sprite = lootDefComp.GetValue<LootDef>().Sprite;
        //         SetAlpha(imageComp.Value, 1);
        //     }
        // } else {
        //     ref var imageComp = ref toolPanel.GetComponent<ImageComponent>();
        //     ref var activeItemDefComp = ref toolPanel.GetComponent<ItemDefinitionComponent>();
        //     if (!activeItemDefComp.Value) {
        //         activeItemDefComp.Value = null;
        //         imageComp.Value.sprite = null;
        //         SetAlpha(imageComp.Value, 0);
        //     }
        // }
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