namespace Spectre.Console;

internal sealed class DefaultInput : IAnsiConsoleInput
{
    private static readonly TimeSpan _keyAvailableDelay = TimeSpan.FromMilliseconds(5);

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
        while (!IsKeyAvailable())
        {
            cancellationToken.ThrowIfCancellationRequested();
            Thread.Sleep(_keyAvailableDelay);
        }

        return System.Console.ReadKey(intercept);
    }

    public async Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancellationToken)
    {
        while (!IsKeyAvailable())
        {
            await Task.Delay(_keyAvailableDelay, cancellationToken).ConfigureAwait(false);
        }

        return System.Console.ReadKey(intercept);
    }
}