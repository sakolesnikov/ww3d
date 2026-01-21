using Friflo.Engine.ECS;

[LevelScope]
public class EndDragSignalImpl : GenericSignal<EndDragSignal> {

    protected override void Signal(Signal<EndDragSignal> signal) {
        // var entity = signal.Entity;
        // var entity = signal.Entity;
        // ref var imageComp = ref entity.GetComponent<ImageComponent>();
        // var transform = entity.GetTransform();
        // var parentTransform = entity.GetComponent<ParentTransformComponent>().Value;
        // imageComp.Value.raycastTarget = true;
        // transform.SetParent(parentTransform);
    }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef is LootDef;

}