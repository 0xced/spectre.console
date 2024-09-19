using System.IO;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Generator.Commands
{
    public class GeneratorSettings : CommandSettings
    {
        [CommandArgument(0, "[OUTPUT]")]
        public DirectoryInfo Output { get; set; }

        public override ValidationResult Validate()
        {
            Output?.Create();
            return base.Validate();
        }
    }
}
