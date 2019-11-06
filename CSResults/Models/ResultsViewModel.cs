using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace CSResults.Models
{
    public class ResultsViewModel
    {
        public Module module { get; set; }
        public Result result { get; set; }
    }
}