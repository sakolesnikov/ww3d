using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;

public class LootDebug : MonoBehaviour, IEntityAware {

    [SerializeField]
    private bool open;
    [Inject]
    private EntityStore world;
    private Entity player;
    private Entity CurrentEntity;

    private void Start() {
        player = world.GetPlayer();
    }

    public void OnEntityReady(ref Entity entity) {
        CurrentEntity = entity;
    }

    private void OnValidate() {
        if (player.IsNull) {
            return;
        }

        if (open) {
            player.AddComponent(new OpenExchangeRequest { Target = CurrentEntity });
        }
    }

}