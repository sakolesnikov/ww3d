using Friflo.Engine.ECS;

[LevelScope]
public class EndDragSignalImpl : GenericSignal<EndDragSignal> {

    protected override void Signal(Signal<EndDragSignal> signal) {
        var entity = signal.Entity;
        ref var imageComp = ref entity.GetComponent<ImageComponent>();
        var transform = entity.GetTransform();
        ref var parentTransformComp = ref entity.GetComponent<ParentTransformComponent>();
        var parentTransform = parentTransformComp.Value;
        parentTransformComp.Value = null;
        imageComp.Value.raycastTarget = true;
        transform.SetParent(parentTransform);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}