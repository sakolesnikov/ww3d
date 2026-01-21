using Friflo.Engine.ECS;

public interface ITriggerEnter : ISelfRegisterable {

    void Enter(Entity iAm, Entity with);

}