using System;
using Friflo.Engine.ECS;

public interface IEntityInitialization : ISelfRegisterable {

    Type getType();

    void Initialize(Entity entity);

}