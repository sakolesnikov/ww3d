using Friflo.Engine.ECS;

[LevelScope]
public class DoorMovementSystem : QueryUpdateSystem<DoorMovementComponent, AnimatorComponent> {

    protected override void OnUpdate() {
        Query.ForEachEntity((ref DoorMovementComponent comp, ref AnimatorComponent animComp, Entity entity) => {
            ref var colliderComp = ref entity.GetComponent<ColliderComponent>();
            AstarPath.active.UpdateGraphs(colliderComp.Value.bounds);
        });
    }

}