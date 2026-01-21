using Friflo.Engine.ECS;

public abstract class GenericSignal<T> : ISignal where T : struct {

    public void AddSignal(Entity entity) {
        entity.AddSignalHandler<T>(Signal);
    }

    public void RemoveSignal(Entity entity) {
        entity.RemoveSignalHandler<T>(Signal);
    }

    protected abstract void Signal(Signal<T> signal);

    public abstract bool IsSupported(Entity entity, EntityDefinition entityDef);

}