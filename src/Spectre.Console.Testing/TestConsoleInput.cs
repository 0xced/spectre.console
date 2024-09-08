namespace Spectre.Console.Testing;

/// <summary>
/// Represents a testable console input mechanism.
/// </summary>
public sealed class TestConsoleInput : IAnsiConsoleInput
{
    private readonly Queue<ConsoleKeyInfo> _input;

    /// <summary>
    /// Initializes a new instance of the <see cref="TestConsoleInput"/> class.
    /// </summary>
    public TestConsoleInput()
    {
        _input = new Queue<ConsoleKeyInfo>();
    }

    /// <summary>
    /// Pushes the specified text to the input queue.
    /// </summary>
    /// <param name="input">The input string.</param>
    public void PushText(string input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        foreach (var character in input)
        {
            PushCharacter(character);
        }
    }

    /// <summary>
    /// Pushes the specified text followed by 'Enter' to the input queue.
    /// </summary>
    /// <param name="input">The input.</param>
    public void PushTextWithEnter(string input)
    {
        PushText(input);
        PushKey(ConsoleKey.Enter);
    }

    /// <summary>
    /// Pushes the specified character to the input queue.
    /// </summary>
    /// <param name="input">The input.</param>
    public void PushCharacter(char input)
    {
        var control = char.IsUpper(input);
        _input.Enqueue(new ConsoleKeyInfo(input, (ConsoleKey)input, false, false, control));
    }

    /// <summary>
    /// Pushes the specified key to the input queue.
    /// </summary>
    /// <param name="input">The input.</param>
    public void PushKey(ConsoleKey input)
    {
        _input.Enqueue(new ConsoleKeyInfo((char)input, input, false, false, false));
    }

    /// <summary>
    /// Pushes the specified key to the input queue.
    /// </summary>
    /// <param name="consoleKeyInfo">The input.</param>
    public void PushKey(ConsoleKeyInfo consoleKeyInfo)
    {
        _input.Enqueue(consoleKeyInfo);
    }

    /// <inheritdoc/>
    public bool IsKeyAvailable()
    {
        return _input.Count > 0;
    }

    /// <inheritdoc/>
    public ConsoleKeyInfo ReadKey(bool intercept, CancellationToken cancellationToken = default)
    {
        if (_input.Count == 0)
        {
            throw new InvalidOperationException("No input available.");
        }

        cancellationToken.ThrowIfCancellationRequested();

        return _input.Dequeue();
    }

    /// <inheritdoc/>
    [Obsolete("This method will be removed in a future release. Use the synchronous ReadKey() method instead.", error: false)]
    public Task<ConsoleKeyInfo> ReadKeyAsync(bool intercept, CancellationToken cancellationToken)
    {
        return Task.FromResult(ReadKey(intercept));
    }
}