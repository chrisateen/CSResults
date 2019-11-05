using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSResults.Models
{

    /*
     * View Model to enable to select all modules for a dropdown list and the filtered module data
    */
    public class ResultsGraphViewModel
    {
        public IEnumerable<Module> modules { get; set; }
        public IEnumerable<ResultsViewModel> resultViewModel { get; set; }
    }
}