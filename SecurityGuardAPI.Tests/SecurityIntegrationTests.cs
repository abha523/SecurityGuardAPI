using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace SecurityGuardAPI.Tests;

public class SecurityIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SecurityIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Request_WithoutAuthorizationHeader_Returns401Unauthorized()
    {
        // Arrange
        var secureUrl = "/api/zapier/receive";

        // Act
        var response = await _client.PostAsync(secureUrl, null);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Request_WithInvalidToken_Returns401Unauthorized()
    {
        // Arrange
        var secureUrl = "/api/zapier/receive";
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "completely_fake_invalid_token_xyz");

        // Act
        var response = await _client.PostAsync(secureUrl, null);

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}
