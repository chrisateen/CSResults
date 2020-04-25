using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSResultsCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CSResultsCore.Controllers
{
    public class DataController : Controller
    {
        private IGenericRepository<Module> _module;
        private IGenericRepository<Result> _result;

        public DataController(IGenericRepository<Module> module,
                                IGenericRepository<Result> result)
        {
            _module = module;
            _result = result;
        }

        public ActionResult Index()
        {
            IList<int> intList = new List<int>() { 10, 20, 30, 40 };

            return Ok(intList);
        }
    }
}