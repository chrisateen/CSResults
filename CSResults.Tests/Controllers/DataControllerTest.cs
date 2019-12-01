using System.Web.Mvc;
using Xunit;
using CSResults.Controllers;
using Moq;
using CSResults.DAL;
using CSResults.Models;
using System.Linq;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace CSResults.Tests.Controllers
{
    public class DataControllerTest
    {
        [Fact]
        public void TableControllerReturnsView()
        {
            // Arrange
            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();
            DataController controller = new DataController(mockModule.Object, mockResult.Object);

            // Act
            ViewResult result = controller.Table() as ViewResult;

            // Assert
            Assert.NotNull(result);
            controller.WithCallTo(x => x.Table()).ShouldRenderDefaultView();
        }

        [Fact]
        public void TableControllerReturnsListOfResultsToView()
        {
            //Arrange
            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();

            DataController dataController = new DataController(mockModule.Object, mockResult.Object);

            //Act
            var tableController = dataController.Table();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(tableController);
            var model = Assert.IsAssignableFrom<IEnumerable<Result>>(
            viewResult.ViewData.Model);

        }

        [Fact]
        public void ModuleDefaultReturnsModuleView()
        {
            //Arrange
            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();

            DataController controller = new DataController(mockModule.Object, mockResult.Object);

            // Act
            ViewResult result = controller.Table() as ViewResult;

            // Assert
            Assert.NotNull(result);
            controller.WithCallTo(x => x.ModuleDefault()).ShouldRenderView("Module");

        }
    }
}
