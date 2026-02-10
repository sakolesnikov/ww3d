using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.UI;
using Transform = UnityEngine.Transform;

public class ExchangeWindow : MonoBehaviour, IEntityAware {

    [SerializeField]
    private GridLayoutGroup anotherContent;
    private Entity entity;
    public Transform AnotherContent => anotherContent.transform;

    public void Close() {
        entity.EmitSignal(new CloseExchangeSignal());
    }

    public void TakeAll() {
        entity.EmitSignal(new TakeAllSignal());
    }

    public void OnEntityReady(ref Entity entity) {
        this.entity = entity;
    }

}