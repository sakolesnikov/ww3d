using Friflo.Engine.ECS;

public struct OpenInventoryCmd : ICommand {

    public void Init(Entity actor) {
        actor.AddComponent(new OpenInventoryRequest());
    }

    public bool IsFinished(Entity actor) => true;

}