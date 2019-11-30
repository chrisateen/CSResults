using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using CSResults.DAL;
using CSResults.Models;

namespace CSResults.Controllers
{
    public class DataController : Controller
    {
        private ModuleContext db = new ModuleContext();

        public ActionResult Table()
        {

            var modRes = db.Result;
 

            return View(modRes);
        }

        public ActionResult ModuleDefault()
        {

            List<Module> moduleLst = db.Module.ToList();
            List<Result> resultLst = db.Result.ToList();


            var modRes = from m in moduleLst
                         join r in resultLst on m.moduleID equals r.moduleID
                         where m.moduleName == "Introduction to Software Development"
                         select new ResultsViewModel { module = m, result = r };

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = moduleLst.OrderBy(m => m.moduleName),
                resultViewModel = modRes
            };

            return View("Module",graphData);
        }

        [HttpPost]
        public ActionResult Module(ResultsGraphViewModel res)
        {

            List<Module> moduleLst = db.Module.ToList();
            List<Result> resultLst = db.Result.ToList();


            var modRes = from m in moduleLst
                         join r in resultLst on m.moduleID equals r.moduleID
                         where m.moduleID == res.moduleID
                         select new ResultsViewModel { module = m, result = r };

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = moduleLst.OrderBy(m => m.moduleName),
                resultViewModel = modRes
            };

            return View(graphData);
        }

        [HttpGet]
        public ActionResult Module(string id)
        {

            List<Module> moduleLst = db.Module.ToList();
            List<Result> resultLst = db.Result.ToList();


            var modRes = from m in moduleLst
                         join r in resultLst on m.moduleID equals r.moduleID
                         where m.moduleID == id
                         select new ResultsViewModel { module = m, result = r };

            //Save all modules names and the filtered module data to the ResultsGraphViewModel
            var graphData = new ResultsGraphViewModel
            {
                modules = moduleLst.OrderBy(m => m.moduleName),
                resultViewModel = modRes
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

