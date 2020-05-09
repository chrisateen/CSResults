using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSResults.Controllers;
using CSResults.Models;
using CSResults.DAL;
using Moq;
using System.Data.Entity;
using Xunit;

namespace CSResults.Tests.Controllers
{
    
    public class RepositoryTest
    {
        
        //Create fake data
        IQueryable<Result> _results = new List<Result>
        {
            new Result { moduleID = "Mod1", modName = "Module 1" }
        }.AsQueryable();

        IQueryable<Module> _modules = new List<Module>
        {
            new Module { moduleID = "Mod1", moduleName = "Module 1"}
        }.AsQueryable();

        //Create Mock DbSet
        Mock<DbSet<Result>> _mockResultSet = new Mock<DbSet<Result>>();
        Mock<DbSet<Module>> _mockModuleSet = new Mock<DbSet<Module>>();

        [Fact]
        public void test1()
        {
            //Setup the DbSet
            _mockResultSet.As<IQueryable<Result>>().Setup(m => m.Provider).Returns(_results.Provider);
            _mockResultSet.As<IQueryable<Result>>().Setup(m => m.Expression).Returns(_results.Expression);
            _mockResultSet.As<IQueryable<Result>>().Setup(m => m.ElementType).Returns(_results.ElementType);
            _mockResultSet.As<IQueryable<Result>>().Setup(m => m.GetEnumerator()).Returns(_results.GetEnumerator());

            _mockModuleSet.As<IQueryable<Module>>().Setup(m => m.Provider).Returns(_modules.Provider);
            _mockModuleSet.As<IQueryable<Module>>().Setup(m => m.Expression).Returns(_modules.Expression);
            _mockModuleSet.As<IQueryable<Module>>().Setup(m => m.ElementType).Returns(_modules.ElementType);
            _mockModuleSet.As<IQueryable<Module>>().Setup(m => m.GetEnumerator()).Returns(_modules.GetEnumerator());

            var mockContext = new Mock<ModuleContext>();
            mockContext.Setup(c => c.Module).Returns(_mockModuleSet.Object);
            mockContext.Setup(c => c.Result).Returns(_mockResultSet.Object);
            mockContext.Setup(x => x.Set<Module>()).Returns(_mockModuleSet.Object);
            mockContext.Setup(x => x.Set<Result>()).Returns(_mockResultSet.Object);

            var service = new GenericRepository<Result>(mockContext.Object);
            var results = service.Get();
            var resultsSize = results.Count();

            Assert.Equal(1, resultsSize);
        }
    }
}
