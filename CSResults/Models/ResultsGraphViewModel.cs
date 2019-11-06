using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CSResults.Models
{

    /*
     * View Model to enable to select all modules for a dropdown list and the filtered module data
    */
    public class ResultsGraphViewModel
    {
        //To store the moduleID that the user selects
        [DisplayName("Select a module")]
        public String moduleID { get; set; }
        public IEnumerable<Module> modules { get; set; }
        public IEnumerable<ResultsViewModel> resultViewModel { get; set; }
    }
}