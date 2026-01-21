using Friflo.Engine.ECS;
using UnityEngine;
using UnityEngine.UI;

public class ExchangeWindow : MonoBehaviour, IEntityAware {

    [SerializeField]
    private GridLayoutGroup anotherContent;
    [SerializeField]
    private GridLayoutGroup playerContent;
    private Entity entity;
    public GridLayoutGroup AnotherContent => anotherContent;
    public GridLayoutGroup PlayerContent => playerContent;

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