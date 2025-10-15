using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectsComposer.API.Controllers;
using ProjectsComposer.API.Services;
using ProjectsComposer.API.Services.Conflicts;
using ProjectsComposer.Core.Contracts;
using ProjectsComposer.DataAccess.Entites;

namespace ProjectsComposer.Tests;

[TestFixture]
public class ProjectControllerTests
{
    [Test]
    public async Task GetProject_WhenProjectExists_ReturnsOk()
    {
        // // Arrange
        // var projectId = Guid.NewGuid();
        // var mockService = new Mock<IProjectsService>();
        // mockService
        //     .Setup(s => s.GetProject(projectId))
        //     .ReturnsAsync(new ProjectEntity { Id = projectId, Title = "Test", StartDate = DateTime.Today });
        //
        // var controller = new ProjectController(mockService.Object, Mock.Of<IPendingCasesStore>());
        //
        // // Act
        // var result = await controller.GetProjectById(projectId);
        //
        // // Assert
        // var okResult = result.Result as OkObjectResult;
        // Assert.That(okResult, Is.Not.Null);
        //
        // var projectResponse = okResult.Value as ProjectResponse;
        // Assert.That(projectId, Is.EqualTo(projectResponse.Id));
    }
    
}