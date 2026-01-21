using System;
using VContainer;

public class LifetimeAttribute : Attribute {

    public LifetimeAttribute(Lifetime lifetime) => Value = lifetime;

    public Lifetime Value { get; }

}