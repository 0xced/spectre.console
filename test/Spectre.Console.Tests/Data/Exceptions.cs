namespace Spectre.Console.Tests.Data;

public static class TestExceptions
{
    public static bool MethodThatThrows(int? number) => throw new InvalidOperationException("Throwing!");

    public static bool GenericMethodThatThrows<T0, T1, TRet>(int? number) => throw new InvalidOperationException("Throwing!");

    public static void ThrowWithInnerException()
    {
        try
        {
            MethodThatThrows(null);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Something threw!", ex);
        }
    }

    public static void ThrowWithGenericInnerException()
    {
        try
        {
            GenericMethodThatThrows<int, float, double>(null);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Something threw!", ex);
        }
    }

    public static void ThrowWithMultipleInnerExceptionsNoStackTrace()
    {
        throw new FormatException(
            "Failed to start", new FormatException(
                "Failed to load configuration", new FormatException(
                    "Failed to parse file", new InvalidOperationException(
                        "(24,6): Unexpected token end-of-line found while expecting a value"))));
    }

    public static void ThrowWithMultipleInnerExceptions()
    {
        try
        {
            LoadConfiguration();
        }
        catch (Exception ex)
        {
            throw new FormatException("Failed to start", ex);
        }
    }

    private static void LoadConfiguration()
    {
        try
        {
            ParseConfiguration();
        }
        catch (Exception ex)
        {
            throw new FormatException("Failed to load configuration", ex);
        }
    }

    private static void ParseConfiguration()
    {
        try
        {
            throw new InvalidOperationException("(24,6): Unexpected token end-of-line found while expecting a value");
        }
        catch (Exception ex)
        {
            throw new FormatException("Failed to parse file", ex);
        }
    }
}
