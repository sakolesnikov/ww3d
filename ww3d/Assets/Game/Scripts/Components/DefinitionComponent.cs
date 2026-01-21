using Friflo.Engine.ECS;

public struct DefinitionComponent : IComponent {

    public EntityDefinition Value;

    public T GetValue<T>() where T : EntityDefinition => (T)Value;

}