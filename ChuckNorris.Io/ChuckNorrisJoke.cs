using Newtonsoft.Json;

namespace ChuckNorris.Io;

/// <summary>
/// Represents a Chuck Norris joke returned by the Chuck Norris API.
/// </summary>
public class ChuckNorrisJoke
{
    /// <summary>
    /// Gets the categories associated with the joke.
    /// </summary>
    [JsonProperty("categories")]
    public string[] Categories { get; init; } = Array.Empty<string>();

    /// <summary>
    /// Gets the date and time when the joke was created.
    /// </summary>
    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Gets the URL of the icon associated with the joke.
    /// </summary>
    [JsonProperty("icon_url")]
    public string IconUrl { get; init; } = string.Empty;

    /// <summary>
    /// Gets the unique identifier of the joke.
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; init; } = string.Empty;

    /// <summary>
    /// Gets the date and time when the joke was last updated.
    /// </summary>
    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; init; }

    /// <summary>
    /// Gets the URL of the joke.
    /// </summary>
    [JsonProperty("url")]
    public string Url { get; init; } = string.Empty;

    /// <summary>
    /// Gets the text content of the joke.
    /// </summary>
    [JsonProperty("value")]
    public string Value { get; init; } = string.Empty;
}