using System.IO;
using System.Threading.Tasks;
using Generator.Models;
using Scriban;
using Spectre.Console.Cli;

namespace Generator.Commands
{
    public sealed class ColorGeneratorCommand : GeneratorCommand<GeneratorSettings>
    {
        public override Task<int> ExecuteAsync(CommandContext context, GeneratorSettings settings)
        {
            string[] templates =
            [
                "ColorPalette.Generated.template",
                "Color.Generated.template",
                "ColorTable.Generated.template",
            ];

            // Read the color model.
            var model = Color.Parse(ReadData("colors.json"));

            foreach (var templateFilename in templates)
            {
                // Parse the Scriban template.
                var (file, text) = GetTemplate(templateFilename);
                var template = Template.Parse(text);

                // Render the template with the model.
                var result = template.Render(new { Colors = model });

                // Write output to file
                var outputPath = Path.Combine(settings.Output.FullName, Path.ChangeExtension(file.Name, "cs"));
                File.WriteAllText(outputPath, result);
            }

            return Task.FromResult(0);
        }
    }
}
