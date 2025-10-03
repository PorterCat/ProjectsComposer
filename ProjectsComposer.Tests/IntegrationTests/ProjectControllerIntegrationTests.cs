using System.Net;
using ProjectsComposer.Core.Contracts;

namespace ProjectsComposer.Tests.IntegrationTests;

[TestFixture]
public class ProjectControllerIntegrationTests
{
    private CustomWebApplicationFactory<Program> _factory;

    [SetUp]
    public void SetUp() =>
        _factory = new CustomWebApplicationFactory<Program>();

    [TearDown]
    public void TearDown() =>
        _factory.Dispose();
    
    [Test]
    public async Task CheckStatus_SendRequest_ShouldReturnOk()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/project/all");
        
        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
    
    [Test]
    public async Task GetAllProjects_ShouldReturnOkAndProjects()
    {
        // Arrange
        HttpClient client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/project/all");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        var projects = System.Text.Json.JsonSerializer.Deserialize<List<ProjectResponse>>(content, new System.Text.Json.JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        Assert.That(projects, Is.Not.Null);
    }
}