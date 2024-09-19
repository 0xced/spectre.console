using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Scriban;
using Spectre.Console;
using Spectre.Console.Cli;
using Spinner = Generator.Models.Spinner;

namespace Generator.Commands
{
    public sealed class SpinnerGeneratorCommand(IAnsiConsole console) : GeneratorCommand<SpinnerGeneratorCommand.Settings>
    {
        public class Settings : GeneratorSettings
        {
            public override ValidationResult Validate()
            {
                Output ??= GetDefaultOutputDirectory();
                return base.Validate();
            }

            private static DirectoryInfo GetDefaultOutputDirectory([CallerFilePath] string path = "")
                => new(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path)!, "..", "..", "..", "..", "src", "Spectre.Console", "Live", "Progress")));
        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            // Update to the latest spinners
            var spinnersFile = GetDataFile("spinners_sindresorhus.json");
            await FetchSpinners(spinnersFile);

            // Read the spinner model.
            var spinners = new List<Spinner>();
            spinners.AddRange(Spinner.Parse(ReadData("spinners_default.json")));
            spinners.AddRange(Spinner.Parse(ReadData("spinners_sindresorhus.json")));

            // Parse the Scriban template.
            var (file, text) = GetTemplate("Spinner.Generated.template");
            var template = Template.Parse(text);

            // Render the template with the model.
            var result = template.Render(new { Spinners = spinners });

            // Write output to file
            var outputPath = Path.Combine(settings.Output.FullName, Path.ChangeExtension(file.Name, "cs"));
            File.WriteAllText(outputPath, result);

            console.WriteLine(new Uri($"file://{outputPath}").ToString());

            return 0;
        }

        private static async Task FetchSpinners(FileInfo file)
        {
            using var http = new HttpClient();
            await using var httpStream = await http.GetStreamAsync("https://raw.githubusercontent.com/sindresorhus/cli-spinners/refs/heads/main/spinners.json");
            await using var outStream = file.OpenWrite();
            await httpStream.CopyToAsync(outStream);
        }
    }
}
