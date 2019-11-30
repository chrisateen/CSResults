using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSResults.Controllers;

namespace CSResults.Tests.Controllers
{
    [TestClass]
    class DataControllerTest
    {
        [TestMethod]
        public void Table()
        {
            // Arrange
            DataController controller = new DataController();

            // Act
            ViewResult result = controller.Table() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
