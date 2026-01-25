using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

//Deprecated
public class CommandProvider : ISelfRegisterable, IInitializable {

    [Inject]
    private readonly IEnumerable<ICommandBuilder> commandBuilders;
    private readonly Dictionary<Type, ICommandBuilder> dicCmdByEntityDef = new();

    public void Initialize() {
        foreach (var commandBuilder in commandBuilders) {
            dicCmdByEntityDef.Add(commandBuilder.GetEntityType(), commandBuilder);
        }
    }

    public PooledCommandQueue GetCommands(EntityDefinition entityDef) {
        if (dicCmdByEntityDef.TryGetValue(entityDef.GetType(), out var builder)) {
            return builder.Build();
        }

        return null;
    }

    public PooledCommandQueue GetCommands() {
        if (dicCmdByEntityDef.TryGetValue(typeof(NoGenericEntityDef), out var builder)) {
            return builder.Build();
        }

        return null;
    }

}