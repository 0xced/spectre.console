namespace Spectre.Console.Testing;

/// <summary>
/// A fake type registrar suitable for testing.
/// </summary>
public sealed class FakeTypeRegistrar : ITypeRegistrar
{
    /// <summary>
    /// Gets all registrations.
    /// </summary>
    public Dictionary<Type, List<Type>> Registrations { get; }

    /// <summary>
    /// Gets all singleton registrations.
    /// </summary>
    public Dictionary<Type, List<object>> Instances { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FakeTypeRegistrar"/> class.
    /// </summary>
    public FakeTypeRegistrar()
    {
        Registrations = new Dictionary<Type, List<Type>>();
        Instances = new Dictionary<Type, List<object>>();
    }

    /// <inheritdoc/>
    public void Register(Type service, Type implementation)
    {
        if (!Registrations.TryAdd(service, new List<Type> { implementation }))
        {
            Registrations[service].Add(implementation);
        }
    }

    /// <inheritdoc/>
    public void RegisterInstance(Type service, object implementation)
    {
        if (!Instances.TryAdd(service, new List<object> { implementation }))
        {
            Instances[service].Add(implementation);
        }
    }

    /// <inheritdoc/>
    public void RegisterLazy(Type service, Func<object> factory)
    {
        if (factory is null)
        {
            throw new ArgumentNullException(nameof(factory));
        }

        if (!Instances.TryAdd(service, new List<object> { factory() }))
        {
            Instances[service].Add(factory());
        }
    }

    /// <inheritdoc/>
    public ITypeResolver Build()
    {
        return new FakeTypeResolver(Registrations, Instances);
    }
}
