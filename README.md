# ChuckNorris.Io .NET Library
[![NuGet](https://img.shields.io/nuget/v/ChuckNorris.Io.svg?label=NuGet)](https://nuget.org/packages/ChuckNorris.Io)
[![Discord Server](https://img.shields.io/discord/1219353398307065986.svg?label=Discord&color=506de2)](https://discord.gg/wKAbqUPUSZ)

## Overview

ChuckNorrisIo is a simple .NET library for interacting with [chucknorris.io](https://api.chucknorris.io) API, allowing you to retrieve random jokes, filter by category, and search for jokes based on specific queries.

## Features

- Fetch random Chuck Norris jokes
- Filter jokes by category
- Search for jokes based on a keyword

## Installation

You can install the library via NuGet Package Manager:

```bash
dotnet add package ChuckNorris.Io
```

Or via the NuGet Package Manager Console:
```bash
Install-Package ChuckNorris.Io
```
## Usage
### Basic usage
Here's how you can fetch a random Chuck Norris joke:

```csharp
var joke = chuckNorrisIo.GetRandomJoke().GetAwaiter().GetResult();
if (joke1 is null)
{
    Console.WriteLine("No jokes... Chuck Norris has already knocked them all out.");
    return;
}
Console.WriteLine($"Random joke: {joke.Value}");
```

### Fetching a Joke by Category
You can also fetch a joke from a specific category:
```csharp
var joke = chuckNorrisIo.GetRandomJoke(JokeCategory.Dev).GetAwaiter().GetResult();
if (joke is null)
{
    Console.WriteLine("No jokes... Chuck Norris has already knocked them all out.");
    return;
}
Console.WriteLine($"Random joke in dev category: {joke.Value}");
```

### Searching for Jokes
Search for jokes that contain a specific keyword:

```csharp
var chuckNorrisClient = new ChuckNorrisIo();
var jokes = await chuckNorrisClient.GetJokesBySearch("italy").GetAwaiter().GetResult();

foreach (var joke in jokes)
{
    Console.WriteLine(joke.Value);
}
```

## Available Categories
The following joke categories are available:

- Animal
- Career
- Celebrity
- Dev
- Explicit
- Fashion
- Food
- History
- Money
- Movie
- Music
- Political
- Religion
- Science
- Sport
- Travel
- None (for fetching jokes without category filter)

## Contribution

Feel free to fork the repository and make improvements or add new features. Pull requests are welcome!

## License

This project is licensed under the MIT License.

# Chuck Norris will find you
[![ChuckNorris](https://i.imgur.com/9AZqSBT.jpeg)]()