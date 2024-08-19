using System.Net;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace ChuckNorris.Io.Tests;

public class ChuckNorrisIoTests
{
    private Mock<HttpMessageHandler> _mock;
    private ChuckNorrisIo _norris;

    [SetUp]
    public void SetUp()
    {
        _mock = new Mock<HttpMessageHandler>();

        var httpClient = new HttpClient(_mock.Object);
        _norris = new ChuckNorrisIo(httpClient);
    }
    
    [Test]
    public async Task GetRandomJoke_ReturnsChuckNorrisJoke()
    {
        const string jokeJson = @"{
          'categories': [],
          'created_at': '2020-01-05 13:42:28.984661',
          'icon_url': 'https://api.chucknorris.io/img/avatar/chuck-norris.png',
          'id': 'yFXpfY2DSfaa8fDbbZBAYA',
          'updated_at': '2020-01-05 13:42:28.984661',
          'url': 'https://api.chucknorris.io/jokes/yFXpfY2DSfaa8fDbbZBAYA',
          'value': 'All men are born equal. Chuck Norris was just born more equal than everyone else.'
        }";
        
        
        _mock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jokeJson),
            });
        
        var result = await _norris.GetRandomJoke();
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo("yFXpfY2DSfaa8fDbbZBAYA"));
            Assert.That(result.Value, Is.EqualTo("All men are born equal. Chuck Norris was just born more equal than everyone else."));
        });
    }
    
    [Test]
    public async Task GetRandomJoke_WithCategory_ReturnsChuckNorrisJoke()
    {
        const string jokeJson = @"{
          'categories': [
            'dev'
          ],
          'created_at': '2020-01-05 13:42:19.324003',
          'icon_url': 'https://api.chucknorris.io/img/avatar/chuck-norris.png',
          'id': 'dpk2_epftfgo0cgpfqcpgq',
          'updated_at': '2020-01-05 13:42:19.324003',
          'url': 'https://api.chucknorris.io/jokes/dpk2_epftfgo0cgpfqcpgq',
          'value': 'Chuck Norris can install iTunes without installing Quicktime.'
        }";

        _mock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.RequestUri!.ToString().Contains("category=dev")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jokeJson),
            });
        
        var result = await _norris.GetRandomJoke(JokeCategory.Dev);
        
        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo("dpk2_epftfgo0cgpfqcpgq"));
            Assert.That(result.Value, Is.EqualTo("Chuck Norris can install iTunes without installing Quicktime."));
            Assert.That(result.Categories, Does.Contain("dev"));
        });
    }
    
    [Test]
    public async Task GetJokesBySearch_ReturnsListOfJokes()
    {
        const string jokesJson = @"{
            'total': 1,
            'result': [
                {
                    'categories': [],
                    'created_at': '2020-01-05 13:42:28.984661',
                    'icon_url': 'https://api.chucknorris.io/img/avatar/chuck-norris.png',
                    'id': 'MGgXlBSTTr6vaBMGA_ZxSA',
                    'updated_at': '2020-01-05 13:42:28.984661',
                    'url': 'https://api.chucknorris.io/jokes/MGgXlBSTTr6vaBMGA_ZxSA',
                    'value': 'Chuck Norris can pilot Evangelion Units 00-02 simultaneously.'
                }
            ]
        }";

        _mock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.RequestUri!.ToString().Contains("query=Evangelion")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jokesJson),
            });
        
        var result = await _norris.GetJokesBySearch("Evangelion");
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Not.Empty);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Id, Is.EqualTo("MGgXlBSTTr6vaBMGA_ZxSA"));
            Assert.That(result[0].Value, Is.EqualTo("Chuck Norris can pilot Evangelion Units 00-02 simultaneously."));
        });
    }
}