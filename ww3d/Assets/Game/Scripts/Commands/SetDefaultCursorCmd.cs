using Friflo.Engine.ECS;

public readonly struct SetDefaultCursorCmd : ICommand {

    private readonly Entity cursor;

    public SetDefaultCursorCmd(Entity cursor) => this.cursor = cursor;

    public void Init(Entity actor) {
        cursor.EmitSignal(new PointerExitSignal());
    }

    public bool IsFinished(Entity actor) => true;

}