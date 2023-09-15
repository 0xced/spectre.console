namespace Spectre.Console.Tests.Unit;

public partial class AnsiConsoleTests
{
    public sealed class Prompt
    {
        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public async Task Should_Return_Default_Value_If_Nothing_Is_Entered(bool expected, bool defaultValue)
        {
            // Given
            var console = new TestConsole().EmitAnsiSequences();
            console.Input.PushKey(ConsoleKey.Enter);

            // When
            var result = await console.ConfirmAsync("Want some prompt?", defaultValue, CancellationToken.None);

            // Then
            result.ShouldBe(expected);
        }
    }

    public sealed class Ask
    {
        [Fact]
        public async Task Should_Return_Correct_DateTime_When_Asked_PL_Culture()
        {
            // Given
            var console = new TestConsole().EmitAnsiSequences();
            console.Input.PushTextWithEnter("1/2/1998");

            // When
            var dateTime = await console.AskAsync<DateTime>(string.Empty, CultureInfo.GetCultureInfo("pl-PL"), CancellationToken.None);

            // Then
            dateTime.ShouldBe(new DateTime(1998, 2, 1));
        }

        [Fact]
        public async Task Should_Return_Correct_DateTime_When_Asked_US_Culture()
        {
            // Given
            var console = new TestConsole().EmitAnsiSequences();
            console.Input.PushTextWithEnter("2/1/1998");

            // When
            var dateTime = await console.AskAsync<DateTime>(string.Empty, CultureInfo.GetCultureInfo("en-US"), CancellationToken.None);

            // Then
            dateTime.ShouldBe(new DateTime(1998, 2, 1));
        }
    }
}
