using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CSResults.Models
{
    public class ResultsViewModel
    {
        public Module module { get; set; }
        public Result result { get; set; }
    }
}