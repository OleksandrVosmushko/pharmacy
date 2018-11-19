using System;
using System.Collections.Generic;

namespace courseDb.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            Availability = new HashSet<Availability>();
            Receipt = new HashSet<Receipt>();
        }

        public int Medicineid { get; set; }
        public string Name { get; set; }
        public int? Makerid { get; set; }
        public string Description { get; set; }
        public string Measurement { get; set; }

        public Maker Maker { get; set; }
        public ICollection<Availability> Availability { get; set; }
        public ICollection<Receipt> Receipt { get; set; }
    }
}
