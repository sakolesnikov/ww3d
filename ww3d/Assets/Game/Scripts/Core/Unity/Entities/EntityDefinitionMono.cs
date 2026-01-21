using Friflo.Engine.ECS;
using UnityEngine;

public class EntityDefinitionMono : MonoBehaviour, IEntityAware {

    [SerializeField]
    private EntityDefinition entityDefinition;
    public EntityDefinition EntityDefinition => entityDefinition;

    public void OnEntityReady(ref Entity entity) {
        entity.AddComponent(new EntityName(gameObject.name));
        entity.AddComponent(new DefinitionComponent { Value = entityDefinition });
    }

}