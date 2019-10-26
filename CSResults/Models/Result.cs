﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CSResults.Models
{
    public class Result
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Module Name")]
        public string modName { get; set; }

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
        public double ?below30 { get; set; }

        [DisplayName("Percentage between 30% to 39%")]
        public double ?below40 { get; set; }

        [DisplayName("Percentage between 40% to 49%")]
        public double ?below50 { get; set; }

        [DisplayName("Percentage between 50% to 59%")]
        public double ?below60 { get; set; }

        [DisplayName("Percentage between 60% to 69%")]
        public double ?below70 { get; set; }

        [DisplayName("Percentage between 70% to 79%")]
        public double ?below80 { get; set; }

        [DisplayName("Percentage 80% or above")]
        public double ?above80 { get; set; }

        [DisplayName("Module ID")]
        public string modID { get; set; }
        public virtual Module Module { get; set; }

    }
}