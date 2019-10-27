using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CSResults.DAL;
using CSResults.Models;

namespace CSResults.Controllers
{
    public class ModulesController : Controller
    {
        private ModuleContext db = new ModuleContext();

        public ActionResult Index()
        {

            List<Module> moduleLst = db.Module.ToList();
            List<Result> resultLst = db.Result.ToList();

            var modRes = from m in moduleLst
                         join r in resultLst on m.moduleID equals r.modID
                         orderby m.moduleName,r.year descending
                         select new ResultsViewModel { module = m, result = r };

            return View(modRes);
        }

        public ActionResult Graph()
        {
            List<Module> moduleLst = db.Module.ToList();
            List<Result> resultLst = db.Result.ToList();

            var modRes = from m in moduleLst
                         join r in resultLst on m.moduleID equals r.modID
                         where m.moduleName == "Introduction to Software Development"
                         select new ResultsViewModel { module = m, result = r };

            return View(modRes);
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

