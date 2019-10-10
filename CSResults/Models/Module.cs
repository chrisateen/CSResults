using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSResults.Models
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string moduleID { get; set; }
        public string moduleName { get; set; }

        public virtual ICollection<Result> Results { get; set; }
    }
}