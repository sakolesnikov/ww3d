using UnityEngine;

[LevelScope]
public class CursorService : ISelfRegisterable {

    public void LookUp() {
        // SetCursor(cursorDef.LookUp);
    }

    public void Default() {
        SetCursor(null);
    }

    public void SetCursor(Texture2D texture) {
        Cursor.SetCursor(texture, Vector2.zero, CursorMode.Auto);
    }

}