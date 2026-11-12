using System;
using System.Collections.Generic;

namespace CashFlowApp.Models
{
    public partial class DayCashIn
    {
        public int DcashInId { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string AddedBy { get; set; }
        public int ProjectId { get; set; }

        public virtual Projects Project { get; set; }
    }
}
