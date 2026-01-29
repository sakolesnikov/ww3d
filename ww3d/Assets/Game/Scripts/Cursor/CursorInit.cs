using System;
using Friflo.Engine.ECS;
using VContainer;

[LevelScope]
public class CursorInit : IEntityInitialization, IDisposable {

    [Inject]
    private EntityStore world;
    [Inject]
    private CursorService cursorService;

    public void Initialize(Entity entity) {
        entity.AddComponent(new CursorComponent());
        entity.OnComponentChanged += OnComponentChanged;
    }

    private void OnComponentChanged(ComponentChanged ev) {
        if (ev.Type == typeof(AnimationComponent)) {
            if (ev.Action == ComponentChangedAction.Remove) {
                var animComp = ev.OldComponent<AnimationComponent>();
                animComp.Frames = null;
            }

            if (ev.Action == ComponentChangedAction.Add) {
                var animComp = ev.Component<AnimationComponent>();
                if (animComp.Frames.Length > 0) {
                    cursorService.SetCursor(animComp.Frames[0]);
                }
            }
        }
    }

    public void Dispose() {
        if (world.GetCursor() is { IsNull: false } e) {
            e.OnComponentChanged -= OnComponentChanged;
        }
    }

    public Type getType() => typeof(CursorDef);

}