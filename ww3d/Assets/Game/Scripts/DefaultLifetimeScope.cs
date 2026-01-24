using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Friflo.Engine.ECS;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public abstract class DefaultLifetimeScope<T> : LifetimeScope, INonRegisterable where T : Attribute {

    protected abstract void Config(IContainerBuilder builder);

    protected override void Configure(IContainerBuilder builder) {
        var world = new EntityStore();
        // world.EventRecorder.Enabled = true;
        builder.RegisterInstance(world);

        var assembly = Assembly.GetExecutingAssembly();
        LoadSelfRegisterable(builder, assembly);
        LoadSystems(builder, assembly);
        LoadMonos(builder, assembly);
        Config(builder);

        builder.RegisterEntryPoint<WorldEntryPoint>();
    }

    private void LoadSystems(IContainerBuilder builder, Assembly assembly) {
        var systemTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<T>() != null
                        && (typeof(IFixedUpdateSystem).IsAssignableFrom(t)
                            || typeof(IUpdateSystem).IsAssignableFrom(t)
                            || typeof(IDisposeSystem).IsAssignableFrom(t)
                            || typeof(IInitSystem).IsAssignableFrom(t))).ToList();
        systemTypes = Sort(systemTypes);
        foreach (var systemType in systemTypes) {
            builder.Register(systemType, Lifetime.Singleton).AsImplementedInterfaces();
        }
    }

    private void LoadSelfRegisterable(IContainerBuilder builder, Assembly assembly) {
        var types = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<T>() != null && typeof(ISelfRegisterable).IsAssignableFrom(t))
            .ToList();
        types = Sort(types);
        foreach (var type in types) {
            var lifetime = GetLifetime(type);
            builder.Register(type, lifetime).AsImplementedInterfaces().AsSelf();
        }
    }

    private List<Type> Sort(List<Type> types) {
        return types.OrderBy(system => {
            const int DefaultOrder = int.MaxValue;
            var orderAttribute = system.GetCustomAttribute<OrderAttribute>();
            return orderAttribute?.Order ?? DefaultOrder;
        }).ToList();
    }

    private Lifetime GetLifetime(Type type) {
        var lifetimeAttr = type.GetCustomAttribute<LifetimeAttribute>();
        return lifetimeAttr != null ? lifetimeAttr.Value : Lifetime.Singleton;
    }

    private void LoadMonos(IContainerBuilder builder, Assembly assembly) {
        var monoBehaviourType = typeof(MonoBehaviour);
        var monos = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(monoBehaviourType) &&
                        !typeof(INonRegisterable).IsAssignableFrom(t))
            .ToList();
        builder.RegisterBuildCallback(resolver => {
            foreach (var mono in monos) {
                var found = FindObjectsByType(mono, FindObjectsInactive.Include, FindObjectsSortMode.None);
                foreach (var monoBehaviour in found) {
                    resolver.Inject(monoBehaviour);
                }
            }
        });
    }

}