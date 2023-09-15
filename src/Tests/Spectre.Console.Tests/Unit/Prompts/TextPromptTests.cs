namespace Spectre.Console.Tests.Unit;

[ExpectationPath("Prompts/Text")]
public sealed class TextPromptTests
{
    [Fact]
    public async Task Should_Return_Entered_Text()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushTextWithEnter("Hello World");

        // When
        var result = await console.PromptAsync(new TextPrompt<string>("Enter text:"), CancellationToken.None);

        // Then
        result.ShouldBe("Hello World");
    }

    [Fact]
    [Expectation("ConversionError")]
    public async Task Should_Return_Validation_Error_If_Value_Cannot_Be_Converted()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushTextWithEnter("ninety-nine");
        console.Input.PushTextWithEnter("99");

        // When
        await console.PromptAsync(new TextPrompt<int>("Age?"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Lines);
    }

    [Fact]
    [Expectation("DefaultValue")]
    public async Task Should_Chose_Default_Value_If_Nothing_Is_Entered()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .AddChoice("Banana")
                .AddChoice("Orange")
                .DefaultValue("Banana"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("InvalidChoice")]
    public async Task Should_Return_Error_If_An_Invalid_Choice_Is_Made()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushTextWithEnter("Apple");
        console.Input.PushTextWithEnter("Banana");

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .AddChoice("Banana")
                .AddChoice("Orange")
                .DefaultValue("Banana"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("AcceptChoice")]
    public async Task Should_Accept_Choice_In_List()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushTextWithEnter("Orange");

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .AddChoice("Banana")
                .AddChoice("Orange")
                .DefaultValue("Banana"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("AutoComplete_Empty")]
    public async Task Should_Auto_Complete_To_First_Choice_If_Pressing_Tab_On_Empty_String()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushKey(ConsoleKey.Tab);
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .AddChoice("Banana")
                .AddChoice("Orange")
                .DefaultValue("Banana"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("AutoComplete_BestMatch")]
    public async Task Should_Auto_Complete_To_Best_Match()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushText("Band");
        console.Input.PushKey(ConsoleKey.Tab);
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .AddChoice("Banana")
                .AddChoice("Bandana")
                .AddChoice("Orange"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("AutoComplete_NextChoice")]
    public async Task Should_Auto_Complete_To_Next_Choice_When_Pressing_Tab_On_A_Match()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushText("Apple");
        console.Input.PushKey(ConsoleKey.Tab);
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .AddChoice("Apple")
                .AddChoice("Banana")
                .AddChoice("Orange"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("AutoComplete_PreviousChoice")]
    public async Task Should_Auto_Complete_To_Previous_Choice_When_Pressing_ShiftTab_On_A_Match()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushText("Ban");
        console.Input.PushKey(ConsoleKey.Tab);
        console.Input.PushKey(ConsoleKey.Tab);
        var shiftTab = new ConsoleKeyInfo((char)ConsoleKey.Tab, ConsoleKey.Tab, true, false, false);
        console.Input.PushKey(shiftTab);
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .AddChoice("Banana")
                .AddChoice("Bandana")
                .AddChoice("Orange"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("CustomValidation")]
    public async Task Should_Return_Error_If_Custom_Validation_Fails()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushTextWithEnter("22");
        console.Input.PushTextWithEnter("102");
        console.Input.PushTextWithEnter("ABC");
        console.Input.PushTextWithEnter("99");

        // When
        await console.PromptAsync(
            new TextPrompt<int>("Guess number:")
                .ValidationErrorMessage("Invalid input")
                .Validate(age =>
                {
                    if (age < 99)
                    {
                        return ValidationResult.Error("Too low");
                    }
                    else if (age > 99)
                    {
                        return ValidationResult.Error("Too high");
                    }

                    return ValidationResult.Success();
                }), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("CustomConverter")]
    public async Task Should_Use_Custom_Converter()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushTextWithEnter("Banana");

        // When
        var result = await console.PromptAsync(
            new TextPrompt<(int, string)>("Favorite fruit?")
                .AddChoice((1, "Apple"))
                .AddChoice((2, "Banana"))
                .WithConverter(testData => testData.Item2), CancellationToken.None);

        // Then
        result.Item1.ShouldBe(2);
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("SecretDefaultValue")]
    public async Task Should_Choose_Masked_Default_Value_If_Nothing_Is_Entered_And_Prompt_Is_Secret()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .Secret()
                .DefaultValue("Banana"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("SecretValueBackspaceNullMask")]
    public async Task Should_Not_Erase_Prompt_Text_On_Backspace_If_Prompt_Is_Secret_And_Mask_Is_Null()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushText("Bananas");
        console.Input.PushKey(ConsoleKey.Backspace);
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .Secret(null), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("SecretDefaultValueCustomMask")]
    public async Task Should_Choose_Custom_Masked_Default_Value_If_Nothing_Is_Entered_And_Prompt_Is_Secret_And_Mask_Is_Custom()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .Secret('-')
                .DefaultValue("Banana"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("SecretDefaultValueNullMask")]
    public async Task Should_Choose_Empty_Masked_Default_Value_If_Nothing_Is_Entered_And_Prompt_Is_Secret_And_Mask_Is_Null()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushKey(ConsoleKey.Enter);

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Favorite fruit?")
                .Secret(null)
                .DefaultValue("Banana"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("NoSuffix")]
    public async Task Should_Not_Append_Questionmark_Or_Colon_If_No_Choices_Are_Set()
    {
        // Given
        var console = new TestConsole();
        console.Input.PushTextWithEnter("Orange");

        // When
        await console.PromptAsync(
            new TextPrompt<string>("Enter command$"), CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("DefaultValueStyleNotSet")]
    public async Task Uses_default_style_for_default_value_if_no_style_is_set()
    {
        // Given
        var console = new TestConsole
        {
            EmitAnsiSequences = true,
        };
        console.Input.PushTextWithEnter("Input");

        var prompt = new TextPrompt<string>("Enter Value:")
                .ShowDefaultValue()
                .DefaultValue("default");

        // When
        await console.PromptAsync(prompt, CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("DefaultValueStyleSet")]
    public async Task Uses_specified_default_value_style()
    {
        // Given
        var console = new TestConsole
        {
            EmitAnsiSequences = true,
        };
        console.Input.PushTextWithEnter("Input");

        var prompt = new TextPrompt<string>("Enter Value:")
                .ShowDefaultValue()
                .DefaultValue("default")
                .DefaultValueStyle(Color.Red);

        // When
        await console.PromptAsync(prompt, CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("ChoicesStyleNotSet")]
    public async Task Uses_default_style_for_choices_if_no_style_is_set()
    {
        // Given
        var console = new TestConsole
        {
            EmitAnsiSequences = true,
        };
        console.Input.PushTextWithEnter("Choice 2");

        var prompt = new TextPrompt<string>("Enter Value:")
                .ShowChoices()
                .AddChoice("Choice 1")
                .AddChoice("Choice 2");

        // When
        await console.PromptAsync(prompt, CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }

    [Fact]
    [Expectation("ChoicesStyleSet")]
    public async Task Uses_the_specified_choices_style()
    {
        // Given
        var console = new TestConsole
        {
            EmitAnsiSequences = true,
        };
        console.Input.PushTextWithEnter("Choice 2");

        var prompt = new TextPrompt<string>("Enter Value:")
                .ShowChoices()
                .AddChoice("Choice 1")
                .AddChoice("Choice 2")
                .ChoicesStyle(Color.Red);

        // When
        await console.PromptAsync(prompt, CancellationToken.None);

        // Then
        await Verifier.Verify(console.Output);
    }
}
