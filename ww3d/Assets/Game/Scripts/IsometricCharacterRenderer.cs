using UnityEngine;

public class IsometricCharacterRenderer {

    public static readonly string[] staticDirections =
        { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public static readonly string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    private Animator animator;
    private int lastDirection;

    private void Awake() {
        //cache the animator component
        // animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction) {
        string[] directionArray = null;

        if (direction.magnitude < .01f) {
            directionArray = staticDirections;
        } else {
            directionArray = runDirections;
            lastDirection = DirectionToIndex(direction, 8);
        }

        animator.Play(directionArray[lastDirection]);
    }

    public static int DirectionToIndex(Vector2 dir, int sliceCount) {
        var normDir = dir.normalized;
        var step = 360f / sliceCount;
        var halfstep = step / 2;
        var angle = Vector2.SignedAngle(Vector2.up, normDir);
        angle += halfstep;
        if (angle < 0) {
            angle += 360;
        }

        var stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }

    public static int[] AnimatorStringArrayToHashArray(string[] animationArray) {
        var hashArray = new int[animationArray.Length];
        for (var i = 0; i < animationArray.Length; i++) {
            hashArray[i] = Animator.StringToHash(animationArray[i]);
        }

        return hashArray;
    }

}