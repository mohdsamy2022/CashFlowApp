using System;
using System.Collections.Generic;

namespace CashFlowApp.Models
{
    public partial class McashOut
    {
        public int McashOutId { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Fab { get; set; }
        public decimal? March { get; set; }
        public decimal? April { get; set; }
        public decimal? May { get; set; }
        public decimal? June { get; set; }
        public decimal? July { get; set; }
        public decimal? August { get; set; }
        public decimal? Sep { get; set; }
        public decimal? Oct { get; set; }
        public decimal? Nov { get; set; }
        public decimal? Dec { get; set; }
        public int? Year { get; set; }
        public int ProjectId { get; set; }

        public virtual Projects Project { get; set; }
    }
}
