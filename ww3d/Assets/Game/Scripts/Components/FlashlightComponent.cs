using Friflo.Engine.ECS;
using UnityEngine;

public struct FlashlightComponent : IComponent {

    public Light[] Values;

    public void Active() {
        foreach (var l in Values) {
            l.gameObject.SetActive(true);
        }
    }

}