using System;
using Friflo.Engine.ECS;

[LevelScope]
public class ExchangeInit : IEntityInitialization {

    public Type getType() => typeof(ExchangeDef);

    public void Initialize(Entity entity) {
        var go = entity.GetGameObject();
        var invWnd = go.GetComponentInChildren<ExchangeWindow>();
        entity.AddComponent(new ExchangeComponent { Container = invWnd.AnotherContent, Player = invWnd.PlayerContent });
    }

}