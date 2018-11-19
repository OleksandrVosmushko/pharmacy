using System;
using System.Collections.Generic;

namespace courseDb.Models
{
    public partial class Maker
    {
        public Maker()
        {
            Medicine = new HashSet<Medicine>();
        }

        public int Makerid { get; set; }
        public string Makername { get; set; }
        public string Country { get; set; }
        public string Website { get; set; }

        public ICollection<Medicine> Medicine { get; set; }
    }
}
