using System.Text.Json.Serialization;
using FluentAssertions;

namespace MarkdownHack.Tests;

public sealed record TestCase(
    [property: JsonPropertyName("markdown")]
    string Markdown,
    [property: JsonPropertyName("html")]
    string? Html,
    [property: JsonPropertyName("example")]
    int? Example,
    [property: JsonPropertyName("start_line")]
    int? StartLine,
    [property: JsonPropertyName("end_line")]
    int? EndLine,
    [property: JsonPropertyName("section")]
    string? Section);

public class SpecTests
{
    [Theory]
    [InlineData("0.30.0")]
    [InlineData("0.31.2")]
    public async Task Renders_Valid_Html(string specVersion)
    {
        // Arrange
        var testCases = await GetTestCases(specVersion);
        var sut = new MarkdownRenderer();

        // Act, Assert
        testCases
            .Should().NotBeNull()
            .And
            .AllSatisfy(testCase => sut
                .RenderAsHtml(testCase.Markdown)
                    .Should().Be(
                        testCase.Html,
                        "Example {0}, markdown is: '{1}'",
                        testCase.Example,
                        testCase.Markdown));
    }

    private static async Task<TestCase[]?> GetTestCases(string version) => 
        await typeof(SpecTests).Assembly.GetJsonResource<TestCase[]>($"Cases.spec.{version}.json");
}