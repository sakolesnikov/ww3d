using Friflo.Engine.ECS;
using UnityEngine;

public interface ICollisionEnter : ISelfRegisterable {

    void Enter(Entity iAm, Entity with, Collision2D collision);

}