using System.Web.Mvc;
using Xunit;
using CSResults.Controllers;
using Moq;
using CSResults.DAL;
using CSResults.Models;
using System.Linq;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using System.Data.Entity;
using System;
using System.Linq.Expressions;

namespace CSResults.Tests.Controllers
{
    public class DataControllerTest
    {
        [Fact]
        public void TableControllerReturnsCorrectViewandViewModel()
        {
            // Arrange
            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();
            DataController controller = new DataController(mockModule.Object, mockResult.Object);

            // Act
            ViewResult result = controller.Table() as ViewResult;

            // Assert
            Assert.NotNull(result);
            controller.WithCallTo(x => x.Table()).ShouldRenderDefaultView().
                WithModel<Result[]>();
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

        [Fact]
        public void SearchModuleIfCorrectModIDIsPassed()
        {
            //Arrange

            var testContext = new ModuleContext();
            testContext.Result.Add(new Result { moduleID = "Mod1", modName = "Module 1" });
            testContext.Result.Add(new Result { moduleID = "Mod2", modName = "Module 2" });
            testContext.Module.Add(new Module { moduleID = "Mod1", moduleName = "Module 1" });

            //Mock<ModuleContext> mockDb = new Mock<ModuleContext>();

            //Mock<GenericRepository<Result>> mockResult = new Mock<GenericRepository<Result>>(mockDb.Object);
            //Mock<GenericRepository<Module>> mockModule = new Mock<GenericRepository<Module>>(mockDb.Object);

            //var data = new List<Result>{
            //    new Result { moduleID = "Mod1", modName = "Module 1" ,
            //                 Module = new Module { moduleID = "Mod1", moduleName = "Module 1"} }
            //};

            //var mockSet = new Mock<DbSet<Result>>();
            //mockSet.As<IQueryable<Result>>().Setup(m => m.Provider).Returns(data.Provider);
            //mockSet.As<IQueryable<Result>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockSet.As<IQueryable<Result>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockSet.As<IQueryable<Result>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            //mockDb.Setup(m => m.Set<Result>()).Returns(mockSet.Object);

            // mockResult.Setup(x => x.Get(It.IsAny<Expression<Func<Result, bool>>>(),
            //   null, 
            //  null)).Returns(data);

            var res = new GenericRepository<Result>(testContext);
            var mod = new GenericRepository<Module>(testContext);

            DataController controller = new DataController(mod, res);

            controller.WithCallTo(x => x.Module("Mod1")).ShouldRenderDefaultView().
                WithModel<ResultsGraphViewModel>(); ;

        }

        [Fact]
        public void SearchModuleIfNullModIdIsPassed()
        {
            //Arrange
            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();

            DataController controller = new DataController(mockModule.Object, mockResult.Object);

            string str = null;

            controller.WithCallTo(x => x.Module(str)).ShouldRedirectTo(x => x.ModuleDefault());
        }
    }
}
