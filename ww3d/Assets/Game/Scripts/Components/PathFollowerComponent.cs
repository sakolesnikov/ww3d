using System.Collections.Generic;
using Friflo.Engine.ECS;
using UnityEngine;

public struct PathFollowerComponent : IComponent {

    public List<Vector3> Waypoints;
    public int CurrentIndex;
    public Vector3 CurrentTarget => Waypoints[CurrentIndex];
    public bool IsFinished => Waypoints == null || CurrentIndex >= Waypoints.Count;

}