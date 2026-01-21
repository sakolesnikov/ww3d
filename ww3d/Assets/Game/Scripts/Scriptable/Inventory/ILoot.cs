using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

public interface ILoot {

    Sprite Sprite { get; }
    int Amount { get; }

    void Initialize(Entity container, GameObject go, IObjectResolver resolver) { }

}