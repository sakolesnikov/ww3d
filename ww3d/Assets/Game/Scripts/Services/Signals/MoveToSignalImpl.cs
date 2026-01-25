using Friflo.Engine.ECS;

[LevelScope]
public class MoveToSignalImpl : GenericSignal<MoveToSignal> {

    protected override void Signal(Signal<MoveToSignal> signal) { }

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(PlayerDef);

}