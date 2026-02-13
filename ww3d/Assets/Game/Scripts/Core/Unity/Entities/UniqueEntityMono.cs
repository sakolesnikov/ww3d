using Friflo.Engine.ECS;

public class UniqueEntityMono : AbstractEntityMono {

    protected override void PostAwake() {
        var entityDef = GetComponent<EntityDefinitionMono>();
        Entity.AddComponent(new UniqueEntity(entityDef.EntityDefinition.EntityName));
    }

}