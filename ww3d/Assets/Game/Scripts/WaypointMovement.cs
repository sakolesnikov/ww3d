using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour {

    [SerializeField]
    private List<Vector3> waypoints;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float rotationSpeed = 10f;
    [SerializeField]
    private float startMoveAngleThreshold = 3f; // градусы
    private int currentIndex;
    private bool canMove;
    private Animator animator;
    private bool walk;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void SetPath(List<Vector3> newWaypoints) {
        waypoints = newWaypoints;
        currentIndex = 0;
        canMove = false;
    }

    private void Update() {
        if (waypoints == null || waypoints.Count == 0) {
            return;
        }

        UpdateRotationAndMovement();
    }

    private void UpdateRotationAndMovement() {
        var target = waypoints[currentIndex];
        var direction = target - transform.position;
        direction.y = 0f; // если нужен поворот только по Y

        if (direction.sqrMagnitude < 0.01f) {
            currentIndex++;
            if (currentIndex >= waypoints.Count) {
                waypoints.Clear();
                animator.CrossFade("Idle", 0.1f);
                walk = false;
                return;
            }

            canMove = false;
            return;
        }

        var moveDir = direction.normalized;

        // целевой поворот
        var targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);

        // всегда вращаемся
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * 100f * Time.deltaTime
        );

        // проверяем, можно ли начинать движение
        var angle = Quaternion.Angle(transform.rotation, targetRotation);

        if (!canMove) {
            if (angle <= startMoveAngleThreshold) {
                canMove = true;
                if (!walk) {
                    walk = true;
                    animator.CrossFade("Walk", 0.1f);
                }
            } else {
                return; // ещё довернувается — не едем
            }
        }

        // движение
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

}

/*
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovement : MonoBehaviour {

    public List<Vector3> waypoints;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private float rotationSpeed = 20f;
    private int currentIndex;

    private void Update() {
        if (waypoints == null || waypoints.Count == 0) {
            return;
        }

        MoveToWaypoint();
    }

    private void MoveToWaypoint() {
        var target = waypoints[currentIndex];
        var direction = target - transform.position;

        // если почти дошли до точки — переключаемся на следующую
        if (direction.sqrMagnitude < 0.05f * 0.05f) {
            currentIndex++;
            if (currentIndex >= waypoints.Count) {
                waypoints = null;
                currentIndex = 0; // или return; если не нужен цикл
            }

            return;
        }

        // движение
        var moveDir = direction.normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // поворот по направлению движения
        var targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

}
*/