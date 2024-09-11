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
        EnsureInteractive();

        return System.Console.KeyAvailable;
    }

    public ConsoleKeyInfo ReadKey(bool intercept, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        EnsureInteractive();

        while (!System.Console.KeyAvailable)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Thread.Sleep(5);
        }

        return System.Console.ReadKey(intercept);
    }

    public async Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        EnsureInteractive();

        while (!System.Console.KeyAvailable)
        {
            await Task.Delay(5, cancellationToken).ConfigureAwait(false);
        }

        return System.Console.ReadKey(intercept);
    }

    private void EnsureInteractive()
    {
        if (!_profile.Capabilities.Interactive)
        {
            throw new InvalidOperationException("Failed to read input in non-interactive mode.");
        }
    }
}