using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class PointerEnterSignalImpl : GenericSignal<PointerEnterSignal> {

    [Inject]
    private readonly EntityStore world;

    protected override void Signal(Signal<PointerEnterSignal> signal) {
        var entity = signal.Entity;
        if (entity.TryGetComponent<TooltipComponent>(out var tooltipComp)) {
            entity.AddComponent<ShowTooltipIntent>();
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef or IInteractable;

}

[LevelScope]
public class PointerExitSignalImpl : GenericSignal<PointerExitSignal> {

    [Inject]
    private readonly TooltipService tooltipService;

    protected override void Signal(Signal<PointerExitSignal> signal) {
        var entity = signal.Entity;
        if (entity.TryGetComponent<TooltipComponent>(out var tooltipComp)) {
            if (entity.HasComponent<ShowTooltipIntent>()) {
                entity.RemoveComponent<ShowTooltipIntent>();
            } else {
                tooltipService.Hide();
            }
        }
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef or IInteractable;

}

[LevelScope]
public class CursorResetPointerExitSignalImpl : GenericSignal<PointerExitSignal> {

    [Inject]
    private readonly CursorService cursorService;

    protected override void Signal(Signal<PointerExitSignal> signal) {
        cursorService.Default();
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is IInteractable;

}