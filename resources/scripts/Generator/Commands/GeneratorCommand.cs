using System.IO;
using System.Runtime.CompilerServices;
using Spectre.Console.Cli;

namespace Generator.Commands;

public abstract class GeneratorCommand<TSettings> : AsyncCommand<TSettings> where TSettings : GeneratorSettings
{
    protected static FileInfo GetDataFile(string fileName, [CallerFilePath] string path = "")
    {
        return new FileInfo(Path.Combine(Path.GetDirectoryName(path)!, "..", "Data", fileName));
    }

    protected static string ReadData(string fileName)
    {
        return File.ReadAllText(GetDataFile(fileName).FullName);
    }

    protected static (FileInfo File, string Text) GetTemplate(string fileName, [CallerFilePath] string path = "")
    {
        var file = new FileInfo(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(path)!, "..", "Templates", fileName)));
        return (file, File.ReadAllText(file.FullName));
    }
}