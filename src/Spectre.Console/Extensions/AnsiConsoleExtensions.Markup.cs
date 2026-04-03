namespace Spectre.Console;

/// <summary>
/// Contains extension methods for <see cref="IAnsiConsole"/>.
/// </summary>
public static partial class AnsiConsoleExtensions
{
    /// <summary>
    /// Writes the specified markup to the console.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void Markup(this IAnsiConsole console, string format, params object[] args)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        Markup(console, CultureInfo.CurrentCulture, format, args);
    }

    /// <summary>
    /// Writes the specified markup to the console.
    /// <para/>
    /// All interpolation holes which contain a string are automatically escaped.
    /// </summary>
    /// <example>
    /// <code>
    /// string input = args[0];
    /// string output = Process(input);
    /// console.MarkupInterpolated($"[blue]{input}[/] -> [green]{output}[/]");
    /// </code>
    /// </example>
    /// <param name="console">The console to write to.</param>
    /// <param name="value">The interpolated string value to write.</param>
    public static void MarkupInterpolated(this IAnsiConsole console, FormattableString value)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        MarkupInterpolated(console, CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the specified markup to the console.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void Markup(this IAnsiConsole console, IFormatProvider provider, string format, params object[] args)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        Markup(console, string.Format(provider, format, args));
    }

    /// <summary>
    /// Writes the specified markup to the console.
    /// <para/>
    /// All interpolation holes which contain a string are automatically escaped.
    /// </summary>
    /// <example>
    /// <code>
    /// string input = args[0];
    /// string output = Process(input);
    /// console.MarkupInterpolated(CultureInfo.InvariantCulture, $"[blue]{input}[/] -> [green]{output}[/]");
    /// </code>
    /// </example>
    /// <param name="console">The console to write to.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The interpolated string value to write.</param>
    public static void MarkupInterpolated(this IAnsiConsole console, IFormatProvider provider, FormattableString value)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        Markup(console, Console.Markup.EscapeInterpolated(provider, value));
    }

    /// <summary>
    /// Writes the specified markup to the console.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    /// <param name="value">The value to write.</param>
    public static void Markup(this IAnsiConsole console, string value)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        console.Write(new Markup(value));
    }

    /// <summary>
    /// Writes the specified markup, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void MarkupLine(this IAnsiConsole console, string format, params object[] args)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        MarkupLine(console, CultureInfo.CurrentCulture, format, args);
    }

    /// <summary>
    /// Writes the specified markup, followed by the current line terminator, to the console.
    /// <para/>
    /// All interpolation holes which contain a string are automatically escaped.
    /// </summary>
    /// <example>
    /// <code>
    /// string input = args[0];
    /// string output = Process(input);
    /// console.MarkupLineInterpolated($"[blue]{input}[/] -> [green]{output}[/]");
    /// </code>
    /// </example>
    /// <param name="console">The console to write to.</param>
    /// <param name="value">The interpolated string value to write.</param>
    public static void MarkupLineInterpolated(this IAnsiConsole console, FormattableString value)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        MarkupLineInterpolated(console, CultureInfo.CurrentCulture, value);
    }

    /// <summary>
    /// Writes the specified markup, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    /// <param name="value">The value to write.</param>
    public static void MarkupLine(this IAnsiConsole console, string value)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        Markup(console, value + Environment.NewLine);
    }

    /// <summary>
    /// Writes the specified markup, followed by the current line terminator, to the console.
    /// </summary>
    /// <param name="console">The console to write to.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="format">A composite format string.</param>
    /// <param name="args">An array of objects to write.</param>
    public static void MarkupLine(this IAnsiConsole console, IFormatProvider provider, string format, params object[] args)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        Markup(console, provider, format + Environment.NewLine, args);
    }

    /// <summary>
    /// Writes the specified markup, followed by the current line terminator, to the console.
    /// <para/>
    /// All interpolation holes which contain a string are automatically escaped.
    /// </summary>
    /// <example>
    /// <code>
    /// string input = args[0];
    /// string output = Process(input);
    /// console.MarkupLineInterpolated(CultureInfo.InvariantCulture, $"[blue]{input}[/] -> [green]{output}[/]");
    /// </code>
    /// </example>
    /// <param name="console">The console to write to.</param>
    /// <param name="provider">An object that supplies culture-specific formatting information.</param>
    /// <param name="value">The interpolated string value to write.</param>
    public static void MarkupLineInterpolated(this IAnsiConsole console, IFormatProvider provider, FormattableString value)
    {
        if (console is null) throw new ArgumentNullException(nameof(console));

        MarkupLine(console, Console.Markup.EscapeInterpolated(provider, value));
    }
}