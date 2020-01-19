﻿using System.Web.Mvc;
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
            var data = new List<Result>{
                new Result { moduleID = "Mod1", modName = "Module 1" ,
                             Module = new Module { moduleID = "Mod1", moduleName = "Module 1"} }
            };

            Mock<IGenericRepository<Result>> mockResult = new Mock<IGenericRepository<Result>>();
            Mock<IGenericRepository<Module>> mockModule = new Mock<IGenericRepository<Module>>();

            mockResult.Setup(i => i.Get(m => m.moduleID == "Mod1", null, x => x.Module)).Returns(data);

            DataController controller = new DataController(mockModule.Object, mockResult.Object);

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
