using Friflo.Engine.ECS;

public interface ISignal : ISelfRegisterable {

    void AddSignal(Entity entity);

    void RemoveSignal(Entity entity);

    bool IsSupported(Entity entity, EntityDefinition entityDef);

}