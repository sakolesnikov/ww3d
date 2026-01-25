using Friflo.Engine.ECS;
using UnityEngine;

[Order(5)]
public class AnimationUpdateSystem : QueryUpdateSystem<PathFollowerComponent> {

    private static readonly string[] staticDirections =
        { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    private static readonly string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };
    private int lastDirection;
    private Animator animator;
    private readonly int startMoveAngleThreshold = 3;
    private bool walk;
    private bool canMove;

    protected override void OnUpdate() {
        Query.ForEachEntity((ref PathFollowerComponent follower, Entity entity) => {
            var transform = entity.GetTransform();
            ref var animatorComp = ref entity.GetComponent<AnimatorComponent>();

            var moveDir = Vector2.zero;

            if (follower.IsFinished) {
                return;
            }

            var worldDir = follower.CurrentTarget - transform.position;
            moveDir = new Vector2(worldDir.x, worldDir.y).normalized;
            var targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                100f * Time.deltaTime
            );
            var angle = Quaternion.Angle(transform.rotation, targetRotation);

            if (!canMove) {
                if (angle <= startMoveAngleThreshold) {
                    canMove = true;
                    if (!walk) {
                        walk = true;
                        animatorComp.Value.CrossFade("Walk", 0.1f);
                    }
                }
            }

            // UpdateAnimationState(animatorComp.Value, moveDir);
        });
    }

    private void UpdateAnimationState(Animator anim, Vector2 direction) {
        string[] directionArray;
        int directionIndex;

        if (direction.sqrMagnitude < 0.001f) {
            directionArray = staticDirections;
            directionIndex = lastDirection;
        } else {
            directionArray = runDirections;
            directionIndex = DirectionToIndex(direction, 8);
            lastDirection = directionIndex;
        }

        anim.Play(directionArray[directionIndex]);
    }

    // public void SetDirection(Vector2 direction) {
    //     string[] directionArray = null;
    //
    //     if (direction.magnitude < .01f) {
    //         directionArray = staticDirections;
    //     } else {
    //         directionArray = runDirections;
    //         lastDirection = DirectionToIndex(direction, 8);
    //     }
    //
    //     animator.Play(directionArray[lastDirection]);
    // }
    //
    private int DirectionToIndex(Vector2 dir, int sliceCount) {
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
    //
    // public static int[] AnimatorStringArrayToHashArray(string[] animationArray) {
    //     var hashArray = new int[animationArray.Length];
    //     for (var i = 0; i < animationArray.Length; i++) {
    //         hashArray[i] = Animator.StringToHash(animationArray[i]);
    //     }
    //
    //     return hashArray;
    // }

}