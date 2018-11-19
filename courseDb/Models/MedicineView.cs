using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courseDb.Models
{
    public class MedicineView
    {
        public int Medicineid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Measurement { get; set; }
        public string Makername { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }
    }
}
