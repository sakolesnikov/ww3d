using Friflo.Engine.ECS;

public class PathAnimationSystem : QueryUpdateSystem<PathFollowerComponent> {

    protected override void OnUpdate() {
        Query.ForEachEntity((ref PathFollowerComponent follower, Entity entity) => {
            ref var animatorComp = ref entity.GetComponent<AnimatorComponent>();
            if (follower.IsFinished) {
                animatorComp.Value.CrossFade("Idle", 0.1f);
                return;
            }

            // Если стартовали движение — walk, иначе idle (или turn-in-place, если будет)
            if (follower.StartedMoving) {
                animatorComp.Value.CrossFade("Walk", 0.1f);
            }
        });
    }

}