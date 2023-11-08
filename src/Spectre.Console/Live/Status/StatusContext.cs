namespace Spectre.Console;

/// <summary>
/// Represents a context that can be used to interact with a <see cref="Status"/>.
/// </summary>
public sealed class StatusContext
{
    private readonly ProgressContext _context;
    private readonly ProgressTask _task;
    private readonly SpinnerColumn _spinnerColumn;

    /// <summary>
    /// Gets or sets the current status.
    /// </summary>
    public string Status
    {
        get => _task.Description;
        set => SetStatus(value);
    }

    /// <summary>
    /// Gets or sets the current spinner.
    /// </summary>
    public Spinner Spinner
    {
        get => _spinnerColumn.Spinner;
        set => SetSpinner(value);
    }

    /// <summary>
    /// Gets or sets the current spinner style.
    /// </summary>
    public Style? SpinnerStyle
    {
        get => _spinnerColumn.Style;
        set => _spinnerColumn.Style = value;
    }

    internal StatusContext(ProgressContext context, ProgressTask task, SpinnerColumn spinnerColumn)
    {
        _context = context ?? throw new System.ArgumentNullException(nameof(context));
        _task = task ?? throw new System.ArgumentNullException(nameof(task));
        _spinnerColumn = spinnerColumn ?? throw new System.ArgumentNullException(nameof(spinnerColumn));
    }

    /// <summary>
    /// Refreshes the status.
    /// </summary>
    public void Refresh()
    {
        _context.Refresh();
    }

    private void SetStatus(string status)
    {
        ArgumentNullException.ThrowIfNull(status);

        _task.Description = status;
    }

    private void SetSpinner(Spinner spinner)
    {
        ArgumentNullException.ThrowIfNull(spinner);

        _spinnerColumn.Spinner = spinner;
    }
}