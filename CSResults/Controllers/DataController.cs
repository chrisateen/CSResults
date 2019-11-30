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
        private ModuleContext db = new ModuleContext();

        public ActionResult Table()
        {

            var res = db.Result.Include(m => m.Module).OrderBy(r => r.modName);
 
            return View(res.ToList());
        }

        public ActionResult ModuleDefault()
        {

            var res = db.Result.Include(m => m.Module).
                                Where(m => m.modName == "Introduction to Software Development");

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = db.Module.OrderBy(m => m.moduleName),
                Result = res
            };

            return View("Module", graphData);
        }

        [HttpPost]
        public ActionResult Module(ResultsGraphViewModel resFilter)
        {
            var res = db.Result.Include(m => m.Module).Where(m => m.moduleID == resFilter.moduleID);

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = db.Module.OrderBy(m => m.moduleName),
                Result = res
            };

            return View(graphData);

        }

        [HttpGet]
        public ActionResult Module(string id)
        {
            var res = db.Result.Include(m => m.Module).Where(m => m.moduleID == id);

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = db.Module.OrderBy(m => m.moduleName),
                Result = res
            };

            return View(graphData);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}

