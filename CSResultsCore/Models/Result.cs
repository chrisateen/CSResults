using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CSResultsCore.Models
{
    public class Result
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Module Name")]
        public string moduleName { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Academic Year")]
        public string year { get; set; }

        [DisplayName("Mean")]
        public double ?mean { get; set; }

        [DisplayName("Median")]
        public double ?median { get; set; }

        [DisplayName("Percentage below 30%")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double ?below30 { get; set; }

        [DisplayName("Percentage between 30% to 39%")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double ?below40 { get; set; }

        [DisplayName("Percentage between 40% to 49%")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double ?below50 { get; set; }

        [DisplayName("Percentage between 50% to 59%")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double ?below60 { get; set; }

        [DisplayName("Percentage between 60% to 69%")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double ?below70 { get; set; }

        [DisplayName("Percentage between 70% to 79%")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double ?below80 { get; set; }

        [DisplayName("Percentage 80% or above")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double ?above80 { get; set; }

        [DisplayName("Module ID")]
        public string moduleID { get; set; }

        public Module Module { get; set; }

    }
}