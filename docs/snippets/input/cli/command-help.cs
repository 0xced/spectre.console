namespace Snippets.input.cli.command_help;

// begin-snippet: command-help
using System.Collections.Generic;
using System.Threading;
using Spectre.Console;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Help;
using Spectre.Console.Rendering;

public static class Program
{
    public static int Main(string[] args)
    {
        var app = new CommandApp<DefaultCommand>();

        app.Configure(config =>
        {
            // Register the custom help provider
            config.SetHelpProvider(new CustomHelpProvider(config.Settings));
        });

        return app.Run(args);
    }
}

internal class CustomHelpProvider(ICommandAppSettings settings) : HelpProvider(settings)
{
    public override IEnumerable<IRenderable> GetHeader(ICommandModel model, ICommandInfo? command)
    {
        return
        [
            new Text("--------------------------------------"), Text.NewLine,
            new Text("---       CUSTOM HELP HEADER       ---"), Text.NewLine,
            new Text("--------------------------------------"), Text.NewLine,
            Text.NewLine,
        ];
    }
}

internal class DefaultCommand(IAnsiConsole console) : Command
{
    public override int Execute(CommandContext context, CancellationToken cancellationToken)
    {
        console.WriteLine("Hello world");
        return 0;
    }
}
// end-snippet