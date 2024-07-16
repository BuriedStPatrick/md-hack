namespace MarkdownHack;

public sealed class MarkdownRenderer
{
    public string? RenderAsHtml(string markdown)
    {
        // TODO: structured parsing?
        return $"<pre><code>foo\tbaz\t\tbim{Environment.NewLine}</code></pre>{Environment.NewLine}";
    }
}