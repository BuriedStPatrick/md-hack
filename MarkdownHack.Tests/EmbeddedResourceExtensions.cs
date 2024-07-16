using System.Reflection;
using System.Text.Json;

namespace MarkdownHack.Tests;

internal static class EmbeddedResourceExtensions
{
    internal static async Task<TObject?> GetJsonResource<TObject>(this Assembly assembly, string resourceName)
    {
        await using var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{resourceName}");
        if (stream is null)
        {
            return default;
        }

        return await JsonSerializer.DeserializeAsync<TObject>(stream, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true
        });
    }
}