using Friflo.Engine.ECS;

public interface ICommand {

    void Init(Entity actor);

    bool IsFinished(Entity actor);

    virtual void Break(Entity actor) { }

    virtual void CleanUp(Entity actor) { }

}