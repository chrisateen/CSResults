using System.Web.Mvc;
using NUnit.Framework;
using CSResults.Controllers;
using Moq;
using CSResults.DAL;
using CSResults.Models;
using System.Linq;
using System.Collections.Generic;

namespace CSResults.Tests.Controllers
{
    [TestFixture]
    class DataControllerTest
    {
        [Test]
        public void Table()
        {
            // Arrange
            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();
            DataController controller = new DataController(mockModule.Object, mockResult.Object);

            // Act
            ViewResult result = controller.Table() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void TableViewContainsResultsModel()
        {
            //Arrange
            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();

            mockResult.Setup(m => m.GetAll(null, null)).Returns(new[] {
                new Result { modName = "Test 1", year = "2017/18", moduleID = "Mod1"},
                new Result { modName = "Test 2", year = "2017/18", moduleID = "Mod2"}
            }.AsQueryable());

            DataController dataController = new DataController(mockModule.Object, mockResult.Object);

            //Act
            var actual = (IEnumerable<Result>)dataController.Table().Model;

            //Assert
            Assert.IsInstanceOf<IEnumerable<Result>>(actual);

        }
    }
}
