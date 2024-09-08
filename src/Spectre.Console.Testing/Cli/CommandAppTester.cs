using System.Diagnostics.CodeAnalysis;

namespace Spectre.Console.Testing;

/// <summary>
/// A <see cref="CommandApp"/> test harness.
/// </summary>
public sealed class CommandAppTester
{
    private Action<CommandApp>? _appConfiguration;
    private Action<IConfigurator>? _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="CommandAppTester"/> class.
    /// </summary>
    /// <param name="registrar">The registrar.</param>
    public CommandAppTester(ITypeRegistrar? registrar = null)
    {
        Registrar = registrar;
    }

    /// <summary>
    /// Gets or sets the Registrar to use in the CommandApp.
    /// </summary>
    public ITypeRegistrar? Registrar { get; set; }

    /// <summary>
    /// Sets the default command.
    /// </summary>
    /// <param name="description">The optional default command description.</param>
    /// <param name="data">The optional default command data.</param>
    /// <typeparam name="T">The default command type.</typeparam>
    public void SetDefaultCommand<T>(string? description = null, object? data = null)
        where T : class, ICommand
    {
        _appConfiguration = (app) =>
        {
            var defaultCommandBuilder = app.SetDefaultCommand<T>();
            if (description != null)
            {
                defaultCommandBuilder.WithDescription(description);
            }

            if (data != null)
            {
                defaultCommandBuilder.WithData(data);
            }
        };
    }

    /// <summary>
    /// Configures the command application.
    /// </summary>
    /// <param name="action">The configuration action.</param>
    public void Configure(Action<IConfigurator> action)
    {
        if (_configuration != null)
        {
            throw new InvalidOperationException("The command app harnest have already been configured.");
        }

        _configuration = action;
    }

    /// <summary>
    /// Runs the command application and expects an exception of a specific type to be thrown.
    /// </summary>
    /// <typeparam name="T">The expected exception type.</typeparam>
    /// <param name="args">The arguments.</param>
    /// <returns>The information about the failure.</returns>
    public CommandAppFailure RunAndCatch<T>(params string[] args)
        where T : Exception
    {
        var console = new TestConsole().Width(int.MaxValue);

        try
        {
            RunImpl(args, console, async: false, config: c => c.PropagateExceptions()).GetAwaiter().GetResult();
            throw new InvalidOperationException("Expected an exception to be thrown, but there was none.");
        }
        catch (T ex)
        {
            if (ex is CommandAppException commandAppException && commandAppException.Pretty != null)
            {
                console.Write(commandAppException.Pretty);
            }
            else
            {
                console.WriteLine(ex.Message);
            }

            return new CommandAppFailure(ex, console.Output);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                $"Expected an exception of type '{typeof(T).FullName}' to be thrown, "
                + $"but received {ex.GetType().FullName}.");
        }
    }

    /// <summary>
    /// Runs the command application.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <returns>The result.</returns>
    public CommandAppResult Run(params string[] args)
    {
        var console = new TestConsole().Width(int.MaxValue);
        return RunImpl(args, console, async: false).GetAwaiter().GetResult();
    }

    /// <summary>
    /// Runs the command application asynchronously.
    /// </summary>
    /// <param name="args">The arguments.</param>
    /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
    /// <returns>The result.</returns>
    public async Task<CommandAppResult> RunAsync(string[]? args = null, CancellationToken cancellationToken = default)
    {
        var console = new TestConsole().Width(int.MaxValue);
        return await RunImpl(args ?? [], console, async: true, cancellationToken: cancellationToken);
    }

    [SuppressMessage("ReSharper", "MethodHasAsyncOverload")]
    private async Task<CommandAppResult> RunImpl(string[] args, TestConsole console, bool async, Action<IConfigurator>? config = null, CancellationToken cancellationToken = default)
    {
        CommandContext? context = null;
        CommandSettings? settings = null;

        var app = new CommandApp(Registrar);
        _appConfiguration?.Invoke(app);

        if (_configuration != null)
        {
            app.Configure(_configuration);
        }

        if (config != null)
        {
            app.Configure(config);
        }

        app.Configure(c => c.ConfigureConsole(console));
        app.Configure(c => c.SetInterceptor(new CallbackCommandInterceptor((ctx, s) =>
        {
            context = ctx;
            settings = s;
        })));

        int result;
        if (async)
        {
            result = await app.RunAsync(args, cancellationToken);
        }
        else
        {
            result = app.Run(args, cancellationToken);
        }

        var output = console.Output
            .NormalizeLineEndings()
            .TrimLines()
            .Trim();

        return new CommandAppResult(result, output, context, settings);
    }
}