using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Movement : MonoBehaviour {

    private InputSystem_Actions input;
    private Vector3 direction;
    private List<Vector3> waypointsOriginal = new();
    private List<Vector3> waypoints = new();
    private Ray ray;
    private FunnelModifier funnelModifier;
    private Pathfinding.SimpleSmoothModifier simpleSmoothModifier;
    private float lastMoveTime = -1f;

    private void Start() {
        funnelModifier = GetComponent<FunnelModifier>();
        simpleSmoothModifier = GetComponent<Pathfinding.SimpleSmoothModifier>();
        input = new InputSystem_Actions();
        input.Player.Enable();
        // input.Player.Run.performed += OnRunPerformed;
        // input.Player.Move.performed += OnTapPerformed;
    }

    private void OnDestroy() {
        input.Player.Disable();
        // input.Player.Run.performed -= OnRunPerformed;
        // input.Player.Move.performed -= OnTapPerformed;
    }

    private void Update() {
        for (var i = 0; i < waypoints.Count - 1; i++) {
            Debug.DrawLine(waypoints[i], waypoints[i + 1]);
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context) {
        lastMoveTime = Time.time;
        Debug.Log($"Move performed at {lastMoveTime:F3}");
    }

    private void OnRunPerformed(InputAction.CallbackContext context) {
        var currentTime = Time.time;

        if (lastMoveTime >= 0f) {
            var delay = currentTime - lastMoveTime;
            Debug.Log($"Run performed at {currentTime:F3}, delay since Move: {delay:F3} sec");
        } else {
            Debug.Log("Run performed, but Move was not triggered yet");
        }
    }

    // private void OnMovePerformed(InputAction.CallbackContext context) {
    //     Debug.Log("Move performed");
    // }
    //
    // private void OnRunPerformed(InputAction.CallbackContext context) {
    //     Debug.Log("Run performed");
    // }

    private void OnTapPerformed(InputAction.CallbackContext context) {
        var mousePos = Vector2.zero;
        if (context.control.device is not Pointer pointer) {
            return;
        }

        // Debug.Log(context.interaction);
        mousePos = pointer.position.ReadValue();
        ray = Camera.main.ScreenPointToRay(mousePos);
        //
        // return;

        if (Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) {
            Debug.Log(hit.point + ", " + hit.collider.name);
            var path = ABPath.Construct(transform.position, hit.point);

            AstarPath.StartPath(path);
            path.BlockUntilCalculated();

            if (path.error) {
                return;
            }

            var marker = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            marker.transform.position = hit.point;
            marker.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            marker.GetComponent<Renderer>().material.color = Color.red;

            waypointsOriginal = new List<Vector3>(path.vectorPath);
            funnelModifier.Apply(path);
            if (simpleSmoothModifier.enabled) {
                simpleSmoothModifier.Apply(path);
            }

            waypoints = path.vectorPath;
            GetComponent<WaypointMovement>().SetPath(path.vectorPath);
        }


        // for (var i = 0; i < path.vectorPath.Count - 1; i++) {
        // Debug.DrawLine(path.vectorPath[i], path.vectorPath[i + 1]);
        // }
        // var worldPoint = Camera.main.ScreenToWorldPoint(mousePos);
        // direction = worldPoint - transform.position;
        // direction.Normalize();
    }

}