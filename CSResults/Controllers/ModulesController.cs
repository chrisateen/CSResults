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

        // GET: Modules
        public ActionResult Index()
        {

            List<Module> moduleLst = db.Module.ToList();
            List<Result> resultLst = db.Result.ToList();

            var modRes = from m in moduleLst
                         join r in resultLst on m.moduleID equals r.modID
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

        // GET: Modules/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // GET: Modules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResultsViewModel results)
        {
            if (ModelState.IsValid)
            {
                db.Module.Add(results.module);
                db.SaveChanges();

                //Creates link between the module object and result object
                results.result.modID = results.module.moduleID;
                results.result.modName = results.module.moduleName;

                db.Result.Add(results.result);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(results);
        }

        // GET: Modules/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "moduleID,moduleName")] Module module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(module);
        }

        // GET: Modules/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Module module = db.Module.Find(id);
            if (module == null)
            {
                return HttpNotFound();
            }
            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Module module = db.Module.Find(id);
            db.Module.Remove(module);
            db.SaveChanges();
            return RedirectToAction("Index");
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
