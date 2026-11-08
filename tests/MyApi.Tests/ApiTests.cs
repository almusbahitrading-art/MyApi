using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MyApi.Tests;

public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Root_ReturnsHelloDevOps()
    {
        var response = await _client.GetAsync("/");

        response.EnsureSuccessStatusCode();
        Assert.Equal("Hello DevOps!", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Health_ReturnsHealthy()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal("Healthy", await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task Hello_ReturnsGreetingJson()
    {
        var result = await _client.GetFromJsonAsync<HelloResponse>("/api/hello/Ahmed");

        Assert.NotNull(result);
        Assert.Equal("Hello Ahmed", result.Message);
    }

    [Fact]
    public async Task Version_ReturnsVersion()
    {
        var result = await _client.GetFromJsonAsync<VersionResponse>("/version");

        Assert.NotNull(result);
        Assert.Equal("1.0.0", result.Version);
    }
}

public record HelloResponse(string Message);
public record VersionResponse(string Version);