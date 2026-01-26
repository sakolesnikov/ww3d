public interface ITapProvider : ISelfRegisterable {

    int Order { get; }

    bool CanHandle(in TapContext ctx);

    void Build(in TapContext ctx, PooledCommandQueue queue);

}