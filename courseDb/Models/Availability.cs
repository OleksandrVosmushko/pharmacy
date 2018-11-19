using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace courseDb.Models
{
    public partial class Availability
    {
        public int Availabilityid { get; set; }
        public int? Medicineid { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Productiontime { get; set; }

        public Medicine Medicine { get; set; }
    }
}
