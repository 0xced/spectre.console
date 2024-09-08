namespace Spectre.Console;

/// <summary>
/// A console capable of writing ANSI escape sequences.
/// </summary>
public static partial class AnsiConsole
{
    /// <summary>
    /// Displays a prompt to the user.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="prompt">The prompt to display.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    public static T Prompt<T>(IPrompt<T> prompt, CancellationToken cancellationToken = default)
    {
        if (prompt is null)
        {
            throw new ArgumentNullException(nameof(prompt));
        }

        return prompt.Show(Console, cancellationToken);
    }

    /// <summary>
    /// Displays a prompt to the user.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="prompt">The prompt markup text.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    public static T Ask<T>(string prompt, CancellationToken cancellationToken = default)
    {
        return new TextPrompt<T>(prompt).Show(Console, cancellationToken);
    }

    /// <summary>
    /// Displays a prompt to the user with a given default.
    /// </summary>
    /// <typeparam name="T">The prompt result type.</typeparam>
    /// <param name="prompt">The prompt markup text.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The prompt input result.</returns>
    public static T Ask<T>(string prompt, T defaultValue, CancellationToken cancellationToken = default)
    {
        return new TextPrompt<T>(prompt)
            .DefaultValue(defaultValue)
            .Show(Console, cancellationToken);
    }

    /// <summary>
    /// Displays a prompt with two choices, yes or no.
    /// </summary>
    /// <param name="prompt">The prompt markup text.</param>
    /// <param name="defaultValue">Specifies the default answer.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns><c>true</c> if the user selected "yes", otherwise <c>false</c>.</returns>
    public static bool Confirm(string prompt, bool defaultValue = true, CancellationToken cancellationToken = default)
    {
        return new ConfirmationPrompt(prompt)
        {
            DefaultValue = defaultValue,
        }
        .Show(Console, cancellationToken);
    }
}