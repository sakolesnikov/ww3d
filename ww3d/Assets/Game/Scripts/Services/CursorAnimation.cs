using UnityEngine;

public class CursorAnimation : MonoBehaviour {

    public Texture2D[] frames;
    public float frameRate = 0.1f;
    private int currentFrame;
    private float timer;

    private void Start() {
        Cursor.SetCursor(frames[0], Vector2.zero, CursorMode.Auto);
    }

    private void Update() {
        if (frames == null || frames.Length == 0) {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= frameRate) {
            timer -= frameRate;
            currentFrame = (currentFrame + 1) % frames.Length;
            Cursor.SetCursor(frames[currentFrame], Vector2.zero, CursorMode.Auto);
        }
    }

}