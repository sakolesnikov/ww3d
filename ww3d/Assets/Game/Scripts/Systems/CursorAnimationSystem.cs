using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class CursorAnimationSystem : QueryUpdateSystem<AnimationComponent> {

    [Inject]
    private readonly CursorService cursorService;
    [Inject]
    private readonly CursorConfig cursorConfig;

    protected override void OnUpdate() {
        Query.ForEachEntity((ref AnimationComponent comp, Entity entity) => {
            comp.Timer += Time.deltaTime;

            if (comp.Timer >= cursorConfig.FrameRate) {
                comp.Timer -= cursorConfig.FrameRate;
                comp.CurrentFrame = (comp.CurrentFrame + 1) % comp.Frames.Length;
                cursorService.SetCursor(comp.Frames[comp.CurrentFrame]);
            }
        });
    }

}