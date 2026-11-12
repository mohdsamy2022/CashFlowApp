using System;
using System.Collections.Generic;

namespace CashFlowApp.Models
{
    public partial class CashIn
    {
        public int CashInId { get; set; }
        public int Year { get; set; }
        public decimal? TyearCashIn { get; set; }
        public int ProjectId { get; set; }

        public virtual Projects Project { get; set; }
    }
}
