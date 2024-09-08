namespace Spectre.Console;

/// <summary>
/// Represents a prompt.
/// </summary>
/// <typeparam name="T">The prompt result type.</typeparam>
public interface IPrompt<T>
{
    /// <summary>
    /// Shows the prompt.
    /// </summary>
    /// <param name="console">The console.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    T Show(IAnsiConsole console, CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows the prompt asynchronously.
    /// </summary>
    /// <param name="console">The console.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    [Obsolete("This method will be removed in a future release. Use the synchronous Show() method instead.", error: false)]
    Task<T> ShowAsync(IAnsiConsole console, CancellationToken cancellationToken);
}