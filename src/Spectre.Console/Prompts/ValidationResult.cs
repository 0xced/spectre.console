namespace Spectre.Console;

/// <summary>
/// Represents a prompt validation result.
/// </summary>
public sealed class ValidationResult
{
    /// <summary>
    /// Gets a value indicating whether or not validation was successful.
    /// </summary>
    public bool Successful { get; }

    /// <summary>
    /// Gets the validation error message.
    /// </summary>
    [Obsolete("Use the Pretty property instead")]
    public string? Message
    {
        get
        {
            if (Pretty is null)
            {
                return null;
            }

            var writer = new StringWriter();
            var profile = new Profile(new AnsiConsoleOutput(writer), Encoding.Default);
            var context = new RenderContext(profile.Capabilities);
            Pretty.Render(context, maxWidth: int.MaxValue);
            return writer.ToString();
        }
    }

    /// <summary>
    /// Gets the pretty validation error message.
    /// </summary>
    public IRenderable? Pretty { get; }

    private ValidationResult(bool successful, string? message)
        : this(successful, message is null ? null : new Text(message))
    {
    }

    private ValidationResult(bool successful, IRenderable? message)
    {
        Successful = successful;
        Pretty = message;
    }

    /// <summary>
    /// Returns a <see cref="ValidationResult"/> representing successful validation.
    /// </summary>
    /// <returns>The validation result.</returns>
    public static ValidationResult Success()
    {
        return new ValidationResult(true, (IRenderable?)null);
    }

    /// <summary>
    /// Returns a <see cref="ValidationResult"/> representing a validation error.
    /// </summary>
    /// <param name="message">The validation error message, or <c>null</c> to show the default validation error message.</param>
    /// <returns>The validation result.</returns>
    public static ValidationResult Error(string? message = null)
    {
        return new ValidationResult(false, message);
    }

    /// <summary>
    /// Returns a <see cref="ValidationResult"/> representing a validation error.
    /// </summary>
    /// <param name="pretty">The validation error message, or <c>null</c> to show the default validation error message.</param>
    /// <returns>The validation result.</returns>
    public static ValidationResult Error(IRenderable? pretty = null)
    {
        return new ValidationResult(false, pretty);
    }
}