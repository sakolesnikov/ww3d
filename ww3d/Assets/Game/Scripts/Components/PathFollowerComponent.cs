using System.Collections.Generic;
using Friflo.Engine.ECS;
using Pathfinding;
using UnityEngine;

public struct PathFollowerComponent : IComponent {

    public NNInfo TargetNode;
    public List<Vector3> Waypoints;
    public int CurrentIndex;
    public bool StartedMoving;
    public MoveMode MoveMode;
    public Vector3 CurrentTarget => Waypoints[CurrentIndex];
    public bool IsFinished => Waypoints == null || CurrentIndex >= Waypoints.Count;

}