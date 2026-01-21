using Friflo.Engine.ECS;

public interface ITriggerExit : ISelfRegisterable {

    void Exit(Entity iAm, Entity from);

}