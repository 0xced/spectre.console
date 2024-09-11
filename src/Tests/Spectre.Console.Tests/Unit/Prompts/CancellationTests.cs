namespace Spectre.Console.Tests.Unit;

public class CancellationTests
{
    private readonly IAnsiConsole _console = AnsiConsole.Create(new AnsiConsoleSettings { Interactive = InteractionSupport.Yes, Ansi = AnsiSupport.Yes });

    [Fact]
    public void ConfirmationPrompt_Should_Support_Cancellation()
    {
        // Given
        var prompt = new ConfirmationPrompt("");

        // When
        Action action = () => prompt.Show(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }

    [Fact]
    public void TextPrompt_Should_Support_Cancellation()
    {
        // Given
        var prompt = new TextPrompt<string>("");

        // When
        Action action = () => prompt.Show(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }

    [Fact]
    public void MultiSelectionPrompt_Should_Support_Cancellation()
    {
        // Given
        var prompt = new MultiSelectionPrompt<string>();
        prompt.AddChoice("");

        // When
        Action action = () => prompt.Show(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }

    [Fact]
    public void SelectionPrompt_Should_Support_Cancellation()
    {
        // Given
        var prompt = new SelectionPrompt<string>();
        prompt.AddChoice("");

        // When
        Action action = () => prompt.Show(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }

    [Fact]
    public void ConfirmationPrompt_Should_Support_Async_Cancellation()
    {
        // Given
        var prompt = new ConfirmationPrompt("");

        // When
        Func<Task> action = async () => await prompt.ShowAsync(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }

    [Fact]
    public void TextPrompt_Should_Support_Async_Async_Cancellation()
    {
        // Given
        var prompt = new TextPrompt<string>("");

        // When
        Func<Task> action = async () => await prompt.ShowAsync(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }

    [Fact]
    public void MultiSelectionPrompt_Should_Async_Support_Cancellation()
    {
        // Given
        var prompt = new MultiSelectionPrompt<string>();
        prompt.AddChoice("");

        // When
        Func<Task> action = async () => await prompt.ShowAsync(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }

    [Fact]
    public void SelectionPrompt_Should_Support_Async_Cancellation()
    {
        // Given
        var prompt = new SelectionPrompt<string>();
        prompt.AddChoice("");

        // When
        Func<Task> action = async () => await prompt.ShowAsync(_console, new CancellationToken(canceled: true));

        // Then
        action.ShouldThrow<OperationCanceledException>();
    }
}