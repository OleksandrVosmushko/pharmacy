using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace courseDb.Models
{
    public partial class Pharmacist
    {
        public Pharmacist()
        {
            Receipt = new HashSet<Receipt>();
        }

        public int Pharmacistid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public double? Salary { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Birthday { get; set; }

        public ICollection<Receipt> Receipt { get; set; }
    }
}
