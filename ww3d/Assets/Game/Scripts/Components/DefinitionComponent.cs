using Friflo.Engine.ECS;

public struct DefinitionComponent : IComponent {

    public EntityDefinition Value;

    public T GetValue<T>() where T : class => Value as T;

}