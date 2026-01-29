using Friflo.Engine.ECS;

public class ChangeCursorHoverImpl : GenericSignal<HoverEnterSignal> {

    protected override void Signal(Signal<HoverEnterSignal> signal) {
        var hoveredEntity = signal.Entity;
        ref var defComp = ref hoveredEntity.GetComponent<DefinitionComponent>();
        // defComp.GetValue<InteractableDef2>().
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is IInteractable;

}