using System;
using System.Collections.Generic;
using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;
using VContainer;
using VContainer.Unity;

public class WorldEntryPoint : IPostStartable, ITickable, IFixedTickable, IDisposable {

    [Inject]
    private IEnumerable<IFixedUpdateSystem> fixedSystems;
    [Inject]
    private IEnumerable<IInitSystem> initSystems;
    [Inject]
    private IEnumerable<IUpdateSystem> updateSystems;
    [Inject]
    private IEnumerable<IDisposeSystem> disposeSystems;
    [Inject]
    private EntityStore world;
    private SystemRoot fixedSystemsRoot;
    private SystemRoot updateSystemsRoot;

    public void Dispose() {
        foreach (var system in disposeSystems) {
            system.Dispose(world);
        }
    }

    public void FixedTick() {
        fixedSystemsRoot.Update(default);
    }

    public void PostStart() {
        foreach (var system in initSystems) {
            system.Init(world);
        }

        updateSystemsRoot = new SystemRoot(world);
        fixedSystemsRoot = new SystemRoot(world);

        foreach (var fixedSystem in fixedSystems) {
            fixedSystemsRoot.Add((BaseSystem)fixedSystem);
        }

        foreach (var updateSystem in updateSystems) {
            updateSystemsRoot.Add((BaseSystem)updateSystem);
        }
    }

    public void Tick() {
        updateSystemsRoot.Update(default);
    }

}