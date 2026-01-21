using Friflo.Engine.ECS;
using UnityEngine;

public class UniqueEntityMono : AbstractEntityMono {

    [SerializeField]
    private UniqueEntityEnum uniqueEntityEnum;

    protected override void PostAwake() {
        Entity.AddComponent(new UniqueEntity(uniqueEntityEnum.GetValue()));
    }

}