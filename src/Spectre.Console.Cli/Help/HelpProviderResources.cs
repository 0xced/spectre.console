namespace Spectre.Console.Cli.Help;

/// <summary>
/// A strongly-typed resource class, for looking up localized strings, etc.
/// </summary>
internal class HelpProviderResources
{
    private readonly Language _language;

    public HelpProviderResources(Language language)
    {
        _language = language;
    }

    /// <summary>
    /// Gets the localised string for ARGUMENTS.
    /// </summary>
    internal string Arguments => _language switch
    {
        Language.German => "ARGUMENTE",
        Language.French => "ARGUMENTS",
        Language.Swedish => "ARGUMENT",
        _ => "ARGUMENTS",
    };

    /// <summary>
    /// Gets the localised string for COMMAND.
    /// </summary>
    internal string Command => _language switch
    {
        Language.German => "KOMMANDO",
        Language.French => "COMMANDE",
        Language.Swedish => "KOMMANDO",
        _ => "COMMAND",
    };

    /// <summary>
    /// Gets the localised string for COMMANDS.
    /// </summary>
    internal string Commands => _language switch
    {
        Language.German => "KOMMANDOS",
        Language.French => "COMMANDES",
        Language.Swedish => "KOMMANDON",
        _ => "COMMANDS",
    };

    /// <summary>
    /// Gets the localised string for DEFAULT.
    /// </summary>
    internal string Default => _language switch
    {
        Language.German => "STANDARDWERT",
        Language.French => "DÉFAUT",
        Language.Swedish => "STANDARD",
        _ => "DEFAULT",
    };

    /// <summary>
    /// Gets the localised string for DESCRIPTION.
    /// </summary>
    internal string Description => _language switch
    {
        Language.German => "BESCHREIBUNG",
        Language.French => "DESCRIPTION",
        Language.Swedish => "BESKRIVNING",
        _ => "DESCRIPTION",
    };

    /// <summary>
    /// Gets the localised string for EXAMPLES.
    /// </summary>
    internal string Examples => _language switch
    {
        Language.German => "BEISPIELE",
        Language.French => "EXEMPLES",
        Language.Swedish => "EXEMPEL",
        _ => "EXAMPLES",
    };

    /// <summary>
    /// Gets the localised string for OPTIONS.
    /// </summary>
    internal string Options => _language switch
    {
        Language.German => "OPTIONEN",
        Language.French => "OPTIONS",
        Language.Swedish => "VAL",
        _ => "OPTIONS",
    };

    /// <summary>
    /// Gets the localised string for Prints help information.
    /// </summary>
    internal string PrintHelpDescription => _language switch
    {
        Language.German => "Zeigt Hilfe an",
        Language.French => "Affiche l'aide",
        Language.Swedish => "Skriver ut hjälpinformation",
        _ => "Prints help information",
    };

    /// <summary>
    /// Gets the localised string for Prints version information.
    /// </summary>
    internal string PrintVersionDescription => _language switch
    {
        Language.German => "Zeigt Versionsinformationen an",
        Language.French => "Affiche la version",
        Language.Swedish => "Skriver ut versionsnummer",
        _ => "Prints version information",
    };

    /// <summary>
    /// Gets the localised string for USAGE.
    /// </summary>
    internal string Usage => _language switch
    {
        Language.German => "VERWENDUNG",
        Language.French => "UTILISATION",
        Language.Swedish => "ANVÄNDING",
        _ => "USAGE",
    };
}
