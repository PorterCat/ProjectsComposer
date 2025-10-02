using System.Net;

namespace ProjectsComposer.Tests;

[TestFixture]
public class ProjectControllerTests
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
}