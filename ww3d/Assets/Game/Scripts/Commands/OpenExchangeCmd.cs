using Friflo.Engine.ECS;

public struct OpenExchangeCmd : ICommand {

    public Entity Target;

    public void Init(Entity actor) {
        actor.AddComponent(new OpenExchangeRequest { Target = Target });
    }

    public bool IsFinished(Entity actor) => true;

}