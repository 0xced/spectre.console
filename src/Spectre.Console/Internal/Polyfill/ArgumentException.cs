#if !NET7_0_OR_GREATER
namespace Spectre.Console;

internal static class ArgumentException
{
    /// <summary>Throws an exception if <paramref name="argument"/> is null or empty.</summary>
    /// <param name="argument">The string argument to validate as non-null and non-empty.</param>
    /// <param name="paramName">The name of the parameter with which <paramref name="argument"/> corresponds.</param>
    /// <exception cref="System.ArgumentNullException"><paramref name="argument"/> is null.</exception>
    /// <exception cref="System.ArgumentException"><paramref name="argument"/> is empty.</exception>
    public static void ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
        {
            ThrowNullOrEmptyException(argument, paramName);
        }
    }

    [DoesNotReturn]
    private static void ThrowNullOrEmptyException(string? argument, string? paramName)
    {
        ArgumentNullException.ThrowIfNull(argument, paramName);
        throw new System.ArgumentException("The value cannot be an empty string.", paramName);
    }
}
#endif