using CashFlowApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CashFlowApp.Controllers
{
    public class BarChartController : Controller
    {
        private readonly CashFlowdbContext _context;

        public BarChartController(CashFlowdbContext context)
        {
            _context = context;
        }
        public IActionResult Bar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var reportData = _context.McashOut.Where(x=>x.McashOutId==id).FirstOrDefault();
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Jan " + reportData.Year,
                Quantity = (decimal)reportData.Jan
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Fab " + reportData.Year,
                Quantity = (decimal)reportData.Fab
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "March " + reportData.Year,
                Quantity = (decimal)reportData.March
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "April " + reportData.Year,
                Quantity = (decimal)reportData.April
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "May " + reportData.Year,
                Quantity = (decimal)reportData.May
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "June " + reportData.Year,
                Quantity = (decimal)reportData.June
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "July " + reportData.Year,
                Quantity = (decimal)reportData.July
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "August " + reportData.Year,
                Quantity = (decimal)reportData.August
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Sep " + reportData.Year,
                Quantity = (decimal)reportData.Sep
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Oct " + reportData.Year,
                Quantity = (decimal)reportData.Oct
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Nov " + reportData.Year,
                Quantity = (decimal)reportData.Nov
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Dec " + reportData.Year,
                Quantity = (decimal)reportData.Dec
            });
            return View(lstModel);
        }
    }
}
