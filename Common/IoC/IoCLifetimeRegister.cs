namespace zintersid.Common.IoC
{
    /// <summary>
    /// Interface for registering services with a Scoped lifetime.
    /// Scoped services are created once per request.
    /// </summary>
    public interface IScopedRegister
    {
        // Define methods or properties for scoped registration if needed
    }

    /// <summary>
    /// Interface for registering services with a Transient lifetime.
    /// Transient services are created each time they are requested.
    /// </summary>
    public interface ITransientRegister
    {
        // Define methods or properties for transient registration if needed
    }

    /// <summary>
    /// Interface for registering services with a Singleton lifetime.
    /// Singleton services are created once and shared throughout the application's lifetime.
    /// </summary>
    public interface ISingletonRegister
    {
        // Define methods or properties for singleton registration if needed
    }
}