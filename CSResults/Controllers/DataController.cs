using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using CSResults.DAL;
using CSResults.Models;
using System.Data.Entity;

namespace CSResults.Controllers
{
    public class DataController : Controller
    {
        private IGenericRepository<Module> module;
        private IGenericRepository<Result> result;

        public DataController()
        {
            this.module = new GenericRepository<Module>();
            this.result = new GenericRepository<Result>();
        }

        //Constructor for unit Testing
        public DataController(IGenericRepository<Module> module , IGenericRepository<Result> result)
        {
            this.module = module;
            this.result = result;
        }

        public ViewResult Table()
        {
            //Gets all the results
            var res = result.GetAll(x => x.OrderBy(r => r.Module.moduleName),x => x.Module);
 
            return View(res);
        }

        public ActionResult ModuleDefault()
        {
            //Returns results for Introduction to Software Development if no option is selected
            //or when loading the page before the user is able to slect an option
            var res = result.Get(m => m.modName == "Introduction to Software Development",null, x => x.Module);

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = module.GetAll(x => x.OrderBy(r => r.moduleName)),
                Result = res
            };

            return View("Module", graphData);
        }

        [HttpPost]
        public ActionResult Module(ResultsGraphViewModel resFilter)
        {
            //Gets the module selected by the user
            var res = result.Get(m => m.moduleID == resFilter.moduleID, null, x => x.Module);

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = module.GetAll(x => x.OrderBy(r => r.moduleName)),
                Result = res
            };

            return View(graphData);

        }

        [HttpGet]
        public ActionResult Module(string id)
        {
            //Gets the module inputted into the URL
            var res = result.Get(m => m.moduleID == id, null, x => x.Module);

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = module.GetAll(x => x.OrderBy(r => r.moduleName)),
                Result = res
            };

            return View(graphData);
        }

    }

}

