using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChuckNorris.Io;

/// <summary>
/// A client class to interact with the Chuck Norris joke API.
/// </summary>
public class ChuckNorrisIo
{
    private const string ApiUrl = "https://api.chucknorris.io";
    private readonly HttpClient _client;

    /// <summary>
    /// Initializes a new instance of the <see cref="ChuckNorrisIo"/> class using a default <see cref="HttpClient"/>.
    /// </summary>
    public ChuckNorrisIo()
    {
        _client = new HttpClient();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ChuckNorrisIo"/> class using a provided <see cref="HttpClient"/>.
    /// </summary>
    /// <param name="client">An instance of <see cref="HttpClient"/> to be used for HTTP requests.</param>
    public ChuckNorrisIo(HttpClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Asynchronously retrieves a random Chuck Norris joke.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains the random <see cref="ChuckNorrisJoke"/>.</returns>
    public async Task<ChuckNorrisJoke?> GetRandomJoke()
    {
        var response = await _client.GetStringAsync($"{ApiUrl}/jokes/random");

        return JsonConvert.DeserializeObject<ChuckNorrisJoke>(response);
    }

    /// <summary>
    /// Asynchronously retrieves a random Chuck Norris joke from a specific category.
    /// </summary>
    /// <param name="category">The category of the joke to retrieve. If <see cref="JokeCategory.None"/> is provided, a random joke from any category will be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the random <see cref="ChuckNorrisJoke"/> from the specified category.</returns>
    public async Task<ChuckNorrisJoke?> GetRandomJoke(JokeCategory category)
    {
        if (category == JokeCategory.None) return await GetRandomJoke();
        
        var response = await _client.GetStringAsync($"{ApiUrl}/jokes/random?category={category.ToString().ToLower()}");
        return JsonConvert.DeserializeObject<ChuckNorrisJoke>(response);
    }

    /// <summary>
    /// Asynchronously searches for Chuck Norris jokes that match a given query.
    /// </summary>
    /// <param name="query">The search query to use for finding jokes.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="ChuckNorrisJoke"/> objects that match the search query.</returns>
    public async Task<List<ChuckNorrisJoke>> GetJokesBySearch(string query)
    {
        var response = await _client.GetStringAsync($"{ApiUrl}/jokes/search?query={query}");
        
        var jokes = JsonConvert.DeserializeObject<JObject>(response)?["result"]?.ToObject<List<ChuckNorrisJoke>>();
        
        return jokes ?? new List<ChuckNorrisJoke>();
    }
}