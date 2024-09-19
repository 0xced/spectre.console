using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using Generator.Models;
using Scriban;
using Scriban.Runtime;
using Spectre.Console.Cli;

namespace Generator.Commands
{
    public sealed class EmojiGeneratorCommand : GeneratorCommand<EmojiGeneratorCommand.Settings>
    {
        private readonly IHtmlParser _parser = new HtmlParser();

        private readonly Dictionary<string, string> _templates = new Dictionary<string, string>
        {
            { "Emoji.Generated.template", "Emoji.Generated.cs" },
            { "Emoji.Json.template", "emojis.json" }, // For documentation
        };

        public sealed class Settings : GeneratorSettings
        {
            [CommandOption("-i|--input <PATH>")]
            public DirectoryInfo Input { get; set; } = new(Environment.CurrentDirectory);
        }

        public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
        {
            var stream = await FetchEmojis(settings);
            var document = await _parser.ParseDocumentAsync(stream);
            var emojis = Emoji.Parse(document).OrderBy(x => x.Name)
                .Where(emoji => !emoji.HasCombinators)
                .ToList();

            // Render all templates
            foreach (var (templateFilename, outputFilename) in _templates)
            {
                var result = await RenderTemplate(templateFilename, emojis);

                var outputPath = Path.Combine(settings.Output.FullName, outputFilename);
                await File.WriteAllTextAsync(outputPath, result);
            }

            return 0;
        }

        private async Task<Stream> FetchEmojis(Settings settings)
        {

            var file = new FileInfo(Path.Combine(settings.Input.FullName, "emoji-list.html"));
            if (!file.Exists)
            {
                using var http = new HttpClient();
                await using var httpStream = await http.GetStreamAsync("http://www.unicode.org/emoji/charts/emoji-list.html");
                await using var outStream = file.OpenWrite();

                await httpStream.CopyToAsync(outStream);
            }

            return file.OpenRead();
        }

        private static async Task<string> RenderTemplate(string templateFilename, IReadOnlyCollection<Emoji> emojis)
        {
            var text = GetTemplate(templateFilename).Text;

            var template = Template.Parse(text);
            var templateContext = new TemplateContext
            {
                // Because of the insane amount of Emojis,
                // we need to get rid of some secure defaults :P
                LoopLimit = int.MaxValue,
            };

            var scriptObject = new ScriptObject();
            scriptObject.Import(new { Emojis = emojis });
            templateContext.PushGlobal(scriptObject);

            return await template.RenderAsync(templateContext);
        }
    }
}
