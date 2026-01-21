using Friflo.Engine.ECS;
using UnityEngine;

public interface ICollisionExit : ISelfRegisterable {

    void Exit(Entity iAm, Entity from, Collision2D collision);

}