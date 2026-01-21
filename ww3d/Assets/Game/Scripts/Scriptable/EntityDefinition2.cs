public abstract class EntityDefinition2<T> : EntityDefinition where T : EntityDefinition2<T> {

    public static readonly string Name = typeof(T).Name;
    public override string EntityType => Name;

}