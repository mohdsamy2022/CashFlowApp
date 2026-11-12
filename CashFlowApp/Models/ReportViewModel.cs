using System;
using System.Collections.Generic;

namespace CashFlowApp.Models
{
    public class ReportViewModel
    {
        public List<SimpleReportViewModel> SimpleReport { get; set; }
        public McashOut MCashout { get; set; }
    }
}
