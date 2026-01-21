public interface ICommandBuilder : IEntityType, ISelfRegisterable {

    PooledCommandQueue Build();

}