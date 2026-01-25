[LevelScope]
public class GroundMoveTapProvider : ITapProvider {

    public bool CanHandle(in TapContext ctx) => !ctx.HasTarget;

    public void Build(in TapContext ctx, PooledCommandQueue queue) {
        queue.Enqueue(new MoveToCmd
        {
            Target = ctx.TargetPosition
        });
    }

}