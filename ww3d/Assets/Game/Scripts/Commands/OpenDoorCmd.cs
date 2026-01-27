using Friflo.Engine.ECS;

public struct OpenDoorCmd : ICommand {

    public Entity Door;

    public void Init(Entity actor) {
        Door.AddComponent(new OpenDoorRequest());
    }

    public bool IsFinished(Entity actor) => true;

}