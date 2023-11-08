namespace Spectre.Console;

/// <summary>
/// Contains extension methods for <see cref="Layout"/>.
/// </summary>
public static class LayoutExtensions
{
    /// <summary>
    /// Sets the ratio of the layout.
    /// </summary>
    /// <param name="layout">The layout.</param>
    /// <param name="ratio">The ratio.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static Layout Ratio(this Layout layout, int ratio)
    {
        ArgumentNullException.ThrowIfNull(layout);

        layout.Ratio = ratio;
        return layout;
    }

    /// <summary>
    /// Sets the size of the layout.
    /// </summary>
    /// <param name="layout">The layout.</param>
    /// <param name="size">The size.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static Layout Size(this Layout layout, int size)
    {
        ArgumentNullException.ThrowIfNull(layout);

        layout.Size = size;
        return layout;
    }

    /// <summary>
    /// Sets the minimum width of the layout.
    /// </summary>
    /// <param name="layout">The layout.</param>
    /// <param name="size">The size.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static Layout MinimumSize(this Layout layout, int size)
    {
        ArgumentNullException.ThrowIfNull(layout);

        layout.MinimumSize = size;
        return layout;
    }
}
