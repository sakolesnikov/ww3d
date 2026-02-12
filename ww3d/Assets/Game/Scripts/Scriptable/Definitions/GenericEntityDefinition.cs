public abstract class GenericEntityDefinition<T> : EntityDefinition where T : GenericEntityDefinition<T> {

    public static readonly string Name = typeof(T).Name;
    public override string EntityName => Name;

}