using System;
using System.Collections.Generic;

namespace CashFlowApp.Models
{
    public partial class Projects
    {
        public Projects()
        {
            CashIn = new HashSet<CashIn>();
            CashOut = new HashSet<CashOut>();
            DayCashIn = new HashSet<DayCashIn>();
            DayCashOut = new HashSet<DayCashOut>();
            McashIn = new HashSet<McashIn>();
            McashOut = new HashSet<McashOut>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? TcashOut { get; set; }
        public decimal? TcashIn { get; set; }
        public decimal? Trevenue { get; set; }

        public virtual ICollection<CashIn> CashIn { get; set; }
        public virtual ICollection<CashOut> CashOut { get; set; }
        public virtual ICollection<DayCashIn> DayCashIn { get; set; }
        public virtual ICollection<DayCashOut> DayCashOut { get; set; }
        public virtual ICollection<McashIn> McashIn { get; set; }
        public virtual ICollection<McashOut> McashOut { get; set; }
    }
}
