using System;
using System.Collections.Generic;

namespace courseDb.Models
{
    public partial class Receipt
    {
        public int Receiptid { get; set; }
        public int? Medicineid { get; set; }
        public int? Pharmacistid { get; set; }
        public int? Quantity { get; set; }

        public Medicine Medicine { get; set; }
        public Pharmacist Pharmacist { get; set; }
    }
}
