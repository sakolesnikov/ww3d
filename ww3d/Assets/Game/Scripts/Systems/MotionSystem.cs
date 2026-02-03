using Friflo.Engine.ECS;
using LitMotion;

[LevelScope]
public class MotionSystem : QueryUpdateSystem<MotionComponent> {

    protected override void OnUpdate() {
        Query.ForEachEntity((ref MotionComponent motionComp, Entity entity) => {
            if (!motionComp.Value.IsPlaying()) {
                CommandBuffer.RemoveComponent<MotionComponent>(entity.Id);
            }
        });
    }

}