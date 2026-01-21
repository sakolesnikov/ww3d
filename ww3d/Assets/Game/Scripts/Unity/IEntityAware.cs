using Friflo.Engine.ECS;

public interface IEntityAware {

    void OnEntityReady(ref Entity entity);

}