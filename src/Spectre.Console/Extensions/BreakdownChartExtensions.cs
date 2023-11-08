namespace Spectre.Console;

/// <summary>
/// Contains extension methods for <see cref="BreakdownChart"/>.
/// </summary>
public static class BreakdownChartExtensions
{
    /// <summary>
    /// Adds an item to the breakdown chart.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="label">The item label.</param>
    /// <param name="value">The item value.</param>
    /// <param name="color">The item color.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart AddItem(this BreakdownChart chart, string label, double value, Color color)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.Data.Add(new BreakdownChartItem(label, value, color));
        return chart;
    }

    /// <summary>
    /// Adds an item to the breakdown chart.
    /// </summary>
    /// <typeparam name="T">A type that implements <see cref="IBreakdownChartItem"/>.</typeparam>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="item">The item.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart AddItem<T>(this BreakdownChart chart, T item)
        where T : IBreakdownChartItem
    {
        ArgumentNullException.ThrowIfNull(chart);

        if (item is BreakdownChartItem chartItem)
        {
            chart.Data.Add(chartItem);
        }
        else
        {
            chart.Data.Add(
                new BreakdownChartItem(
                    item.Label,
                    item.Value,
                    item.Color));
        }

        return chart;
    }

    /// <summary>
    /// Adds multiple items to the breakdown chart.
    /// </summary>
    /// <typeparam name="T">A type that implements <see cref="IBreakdownChartItem"/>.</typeparam>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="items">The items.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart AddItems<T>(this BreakdownChart chart, IEnumerable<T> items)
        where T : IBreakdownChartItem
    {
        ArgumentNullException.ThrowIfNull(chart);

        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
        {
            AddItem(chart, item);
        }

        return chart;
    }

    /// <summary>
    /// Adds multiple items to the breakdown chart.
    /// </summary>
    /// <typeparam name="T">A type that implements <see cref="IBarChartItem"/>.</typeparam>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="items">The items.</param>
    /// <param name="converter">The converter that converts instances of <c>T</c> to <see cref="IBreakdownChartItem"/>.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart AddItems<T>(this BreakdownChart chart, IEnumerable<T> items, Func<T, IBreakdownChartItem> converter)
    {
        ArgumentNullException.ThrowIfNull(chart);

        ArgumentNullException.ThrowIfNull(items);

        ArgumentNullException.ThrowIfNull(converter);

        foreach (var item in items)
        {
            chart.Data.Add(converter(item));
        }

        return chart;
    }

    /// <summary>
    /// Sets the width of the breakdown chart.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="width">The breakdown chart width.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart Width(this BreakdownChart chart, int? width)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.Width = width;
        return chart;
    }

    /// <summary>
    /// Tags will be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="func">The value formatter to use.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart UseValueFormatter(this BreakdownChart chart, Func<double, CultureInfo, string>? func)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.ValueFormatter = func;
        return chart;
    }

    /// <summary>
    /// Tags will be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="func">The value formatter to use.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart UseValueFormatter(this BreakdownChart chart, Func<double, string>? func)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.ValueFormatter = func != null
            ? (value, _) => func(value)
            : null;

        return chart;
    }

    /// <summary>
    /// Tags will be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart ShowPercentage(this BreakdownChart chart)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.ValueFormatter = (value, culture) => string.Format(culture, "{0}%", value);

        return chart;
    }

    /// <summary>
    /// Tags will be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart ShowTags(this BreakdownChart chart)
    {
        return ShowTags(chart, true);
    }

    /// <summary>
    /// Tags will be not be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart HideTags(this BreakdownChart chart)
    {
        return ShowTags(chart, false);
    }

    /// <summary>
    /// Sets whether or not tags will be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="show">Whether or not tags will be shown.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart ShowTags(this BreakdownChart chart, bool show)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.ShowTags = show;
        return chart;
    }

    /// <summary>
    /// Tag values will be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart ShowTagValues(this BreakdownChart chart)
    {
        return ShowTagValues(chart, true);
    }

    /// <summary>
    /// Tag values will be not be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart HideTagValues(this BreakdownChart chart)
    {
        return ShowTagValues(chart, false);
    }

    /// <summary>
    /// Sets whether or not tag values will be shown.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="show">Whether or not tag values will be shown.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart ShowTagValues(this BreakdownChart chart, bool show)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.ShowTagValues = show;
        return chart;
    }

    /// <summary>
    /// Chart and tags is rendered in compact mode.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart Compact(this BreakdownChart chart)
    {
        return Compact(chart, true);
    }

    /// <summary>
    /// Chart and tags is rendered in full size mode.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart FullSize(this BreakdownChart chart)
    {
        return Compact(chart, false);
    }

    /// <summary>
    /// Sets whether or not the chart and tags should be rendered in compact mode.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="compact">Whether or not the chart and tags should be rendered in compact mode.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart Compact(this BreakdownChart chart, bool compact)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.Compact = compact;
        return chart;
    }

    /// <summary>
    /// Sets the <see cref="BreakdownChart.ValueColor"/>.
    /// </summary>
    /// <param name="chart">The breakdown chart.</param>
    /// <param name="color">The <see cref="Color"/> to set.</param>
    /// <returns>The same instance so that multiple calls can be chained.</returns>
    public static BreakdownChart WithValueColor(this BreakdownChart chart, Color color)
    {
        ArgumentNullException.ThrowIfNull(chart);

        chart.ValueColor = color;
        return chart;
    }
}