using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

[LevelScope]
public class CursorService : ISelfRegisterable {

    [Inject]
    private readonly EntityStore world;

    public void Default() {
        RemoveAnimation();
        SetCursor(null);
    }

    private void RemoveAnimation() {
        if (world.GetCursor() is { IsNull : false } cursorEntity) {
            cursorEntity.RemoveComponent<AnimationComponent>();
        }
    }

    public void SetCursor(Texture2D texture) {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

}