using Friflo.Engine.ECS;

public struct CloseDoorCmd : ICommand {

    public Entity Door;

    public void Init(Entity actor) {
        Door.AddComponent(new CloseDoorRequest());
    }

    public bool IsFinished(Entity actor) => true;

}