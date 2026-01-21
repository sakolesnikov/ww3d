using Friflo.Engine.ECS;
using UnityEngine;

[LevelScope]
public class FPSSetInitSystem : IInitSystem {

    public void Init(EntityStore world) {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
    }

}