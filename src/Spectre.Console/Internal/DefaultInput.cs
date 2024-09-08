namespace Spectre.Console;

internal sealed class DefaultInput : IAnsiConsoleInput
{
    private readonly Profile _profile;

    public DefaultInput(Profile profile)
    {
        _profile = profile ?? throw new ArgumentNullException(nameof(profile));
    }

    public bool IsKeyAvailable()
    {
        if (!_profile.Capabilities.Interactive)
        {
            throw new InvalidOperationException("Failed to read input in non-interactive mode.");
        }

        return System.Console.KeyAvailable;
    }

    public ConsoleKeyInfo ReadKey(bool intercept, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        while (!IsKeyAvailable())
        {
            cancellationToken.ThrowIfCancellationRequested();
            Thread.Sleep(5);
        }

        return System.Console.ReadKey(intercept);
    }

    [Obsolete("This method will be removed in a future release. Use the synchronous ReadKey() method instead.", error: true)]
    public Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancellationToken)
    {
        return Task.FromResult(ReadKey(intercept, cancellationToken));
    }
}