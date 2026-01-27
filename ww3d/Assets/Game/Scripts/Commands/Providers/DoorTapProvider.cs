[LevelScope]
public class DoorTapProvider : ITapProvider {

    public bool CanHandle(in TapContext ctx) => ctx.HasTarget && ctx.EntityDef.GetType() == typeof(DoorDef);

    public void Build(in TapContext ctx, PooledCommandQueue queue) {
        queue.Enqueue(new MoveToCmd { Node = ctx.Node, MoveMode = ctx.MoveMode, Target = ctx.TargetPosition });
        if (ctx.TargetEntity.Tags.Has<OpenedTag>()) {
            queue.Enqueue(new CloseDoorCmd { Door = ctx.TargetEntity });
        } else {
            queue.Enqueue(new OpenDoorCmd { Door = ctx.TargetEntity });
        }
    }

    public int Order => 1;

}