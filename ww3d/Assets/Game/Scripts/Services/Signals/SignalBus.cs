using Friflo.Engine.ECS;

public abstract class SignalBus<TSignal> : GenericSignal<TSignal> where TSignal : struct {

    public override bool IsSupported(Entity entity, EntityDefinition entityDef) => entityDef.GetType() == typeof(SignalBusDef);

}