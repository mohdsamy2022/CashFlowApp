using System;
using System.Collections.Generic;

namespace CashFlowApp.Models
{
    public partial class CashOut
    {
        public int CashOutId { get; set; }
        public int Year { get; set; }
        public decimal? TyearCashOut { get; set; }
        public int ProjectId { get; set; }

        public virtual Projects Project { get; set; }
    }
}
