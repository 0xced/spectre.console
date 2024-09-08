namespace Spectre.Console;

/// <summary>
/// Represents the console's input mechanism.
/// </summary>
public interface IAnsiConsoleInput
{
    /// <summary>
    /// Gets a value indicating whether or not
    /// there is a key available.
    /// </summary>
    /// <returns><c>true</c> if there's a key available, otherwise <c>false</c>.</returns>
    bool IsKeyAvailable();

    /// <summary>
    /// Reads a key from the console.
    /// </summary>
    /// <param name="intercept">
    /// Determines whether to display the pressed key in the console window.
    /// <see langword="true"/> to not display the pressed key; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The key that was read.</returns>
    ConsoleKeyInfo ReadKey(bool intercept, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reads a key from the console.
    /// </summary>
    /// <param name="intercept">
    /// Determines whether to display the pressed key in the console window.
    /// <see langword="true"/> to not display the pressed key; otherwise, <see langword="false"/>.
    /// </param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The key that was read.</returns>
    [Obsolete("This method will be removed in a future release. Use the synchronous ReadKey() method instead.", error: false)]
    Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancellationToken);
}