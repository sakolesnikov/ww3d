public interface ITapProvider : ISelfRegisterable {

    bool CanHandle(in TapContext ctx);

    void Build(in TapContext ctx, PooledCommandQueue queue);

}