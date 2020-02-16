using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CSResultsCore.Models
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Module ID")]
        public string moduleID { get; set; }

        [DisplayName("Module Name")]
        public string moduleName { get; set; }

        public ICollection<Result> Results { get; set; }
    }
}