using UnityEngine;
using VContainer;

[LevelScope]
public class CursorService : ISelfRegisterable {

    [Inject]
    private readonly CursorDef cursorDef;

    public void LookUp() {
        SetCursor(cursorDef.LookUp);
    }

    public void Default() {
        SetCursor(null);
    }

    private void SetCursor(Texture2D texture) {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

}