using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class TooltipService : ISelfRegisterable {

    [Inject]
    private readonly EntityStore world;

    private void EmitSignal<T>(T signal) where T : struct {
        if (world.GetTooltip() is { IsNull: false } tooltipEntity) {
            tooltipEntity.EmitSignal(signal);
        } else {
            Debug.LogWarning("Tooltip entity not found");
        }
    }

    public void Show(string key) {
        EmitSignal(new ShowTooltipSignal { Key = key });
    }

    public void Hide() {
        EmitSignal(new HideTooltipSignal());
    }

}