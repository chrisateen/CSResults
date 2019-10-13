using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSResults.Models
{
    public class Result
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string modName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string year { get; set; }

        public double ?mean { get; set; }

        public double ?median { get; set; }

        public double ?below30 { get; set; }

        public double ?below40 { get; set; }

        public double ?below50 { get; set; }

        public double ?below60 { get; set; }

        public double ?below70 { get; set; }

        public double ?below80 { get; set; }

        public double ?above80 { get; set; }
        public string modID { get; set; }
        public virtual Module Module { get; set; }

    }
}