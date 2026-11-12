using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CashFlowApp.Models;
using System.Dynamic;
using System.Xml;

namespace CashFlowApp.Controllers
{
    public class McashOutsController : Controller
    {
        private readonly CashFlowdbContext _context;

        public McashOutsController(CashFlowdbContext context)
        {
            _context = context;
        }

        // GET: McashOuts
        public async Task<IActionResult> Index()
        {
            var DayCashOut = _context.DayCashOut.Include(d => d.Project);
            //var yearlist = _context.DayCashOut.ToList().GroupBy(m => m.Year).Select(x => x.First()).ToList();
            //var yearlist = _context.DayCashOut.Select(x=>x.Year).Distinct().ToList();
            var mcashOut = new McashOut();
            //var projects = _context.Projects.ToList();
            var projects = _context.DayCashOut.Select(x => x.Project).Distinct().ToList();
            var mcashOuts = new List<McashOut>();
            foreach (var project in projects)
            {
                var yearlist = _context.DayCashOut.Where(x=>x.Project==project).Select(x => x.Year).Distinct().ToList();
                foreach (var year in yearlist)
                {
                    mcashOut.Jan = DayCashOut.Where(x => x.Month == 1 && x.Year == year && x.Project==project).Sum(x => x.Amount);
                    mcashOut.Fab = DayCashOut.Where(x => x.Month == 2 && x.Year == year && x.Project==project).Sum(x => x.Amount);
                    mcashOut.March = DayCashOut.Where(x => x.Month == 3 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.April = DayCashOut.Where(x => x.Month == 4 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.May = DayCashOut.Where(x => x.Month == 5 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.June = DayCashOut.Where(x => x.Month == 6 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.July = DayCashOut.Where(x => x.Month == 7 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.August = DayCashOut.Where(x => x.Month == 8 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.Sep = DayCashOut.Where(x => x.Month == 9 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.Oct = DayCashOut.Where(x => x.Month == 10 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.Nov = DayCashOut.Where(x => x.Month == 11 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.Dec = DayCashOut.Where(x => x.Month == 12 && x.Year == year && x.Project == project).Sum(x => x.Amount);
                    mcashOut.Year = year;
                    mcashOut.Project = project;
                    mcashOuts.Add(mcashOut);
                    mcashOut = new McashOut();
                }

            }

            var cashFlowdbContext = _context.McashOut.Include(m => m.Project).ToList();
            if (cashFlowdbContext.Count > 0)
            {
                _context.McashOut.RemoveRange(_context.McashOut);
                await _context.AddRangeAsync(mcashOuts);
                await _context.SaveChangesAsync();
            }
            else
            {
                await _context.AddRangeAsync(mcashOuts);
                await _context.SaveChangesAsync();
            }


            //return View(mcashOuts);
            var cashFlowdbContexts = _context.McashOut.Include(m => m.Project);
            return View(cashFlowdbContexts);
        }

        public IActionResult ViewReport()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name");
            var yearlist = _context.DayCashOut.ToList().GroupBy(m => m.Year).Select(x => x.First()).ToList();
            ViewData["Year"] = new SelectList(yearlist, "Year", "Year");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewReport([Bind("McashOutId,Jan,Fab,March,April,May,June,July,August,Sep,Oct,Nov,Dec,Year,ProjectId")] McashOut mcashOut)
        {
            var DayCashOut = _context.DayCashOut.Include(d => d.Project).Where(x=>x.ProjectId==mcashOut.ProjectId);
            mcashOut.Jan = DayCashOut.Where(x => x.Month == 1 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.Fab = DayCashOut.Where(x => x.Month == 2 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.March = DayCashOut.Where(x => x.Month == 3 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.April = DayCashOut.Where(x => x.Month == 4 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.May = DayCashOut.Where(x => x.Month == 5 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.June = DayCashOut.Where(x => x.Month == 6 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.July = DayCashOut.Where(x => x.Month == 7 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.August = DayCashOut.Where(x => x.Month == 8 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.Sep = DayCashOut.Where(x => x.Month == 9 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.Oct = DayCashOut.Where(x => x.Month == 10 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.Nov = DayCashOut.Where(x => x.Month == 11 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.Dec = DayCashOut.Where(x => x.Month == 12 && x.Year == mcashOut.Year).Sum(x => x.Amount);
            mcashOut.Project = _context.Projects.Where(x => x.ProjectId == mcashOut.ProjectId).FirstOrDefault();
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name");
            var yearlist = _context.DayCashOut.ToList().GroupBy(m => m.Year).Select(x => x.First()).ToList();
            ViewData["Year"] = new SelectList(yearlist, "Year", "Year");
            var mymodel = new ReportViewModel();
            mymodel.MCashout= mcashOut;
            var lstModel = new List<SimpleReportViewModel>();
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Jan " + mcashOut.Year,
                Quantity = (decimal)mcashOut.Jan
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Fab " + mcashOut.Year,
                Quantity = (decimal)mcashOut.Fab
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "March " + mcashOut.Year,
                Quantity = (decimal)mcashOut.March
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "April " + mcashOut.Year,
                Quantity = (decimal)mcashOut.April
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "May " + mcashOut.Year,
                Quantity = (decimal)mcashOut.May
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "June " + mcashOut.Year,
                Quantity = (decimal)mcashOut.June
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "July " + mcashOut.Year,
                Quantity = (decimal)mcashOut.July
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "August " + mcashOut.Year,
                Quantity = (decimal)mcashOut.August
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Sep " + mcashOut.Year,
                Quantity = (decimal)mcashOut.Sep
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Oct " + mcashOut.Year,
                Quantity = (decimal)mcashOut.Oct
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Nov " + mcashOut.Year,
                Quantity = (decimal)mcashOut.Nov
            });
            lstModel.Add(new SimpleReportViewModel
            {
                DimensionOne = "Dec " + mcashOut.Year,
                Quantity = (decimal)mcashOut.Dec
            });
            mymodel.SimpleReport = lstModel;
            return View(mymodel);
        }

        // GET: McashOuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mcashOut = await _context.McashOut
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.McashOutId == id);
            if (mcashOut == null)
            {
                return NotFound();
            }

            return View(mcashOut);
        }

        // GET: McashOuts/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name");
            var yearlist = _context.DayCashOut.ToList().GroupBy(m => m.Year).Select(x=>x.First()).ToList();
            ViewData["MyData"] = new SelectList(yearlist, "Year", "Year");
            return View();
        }

        // POST: McashOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("McashOutId,Jan,Fab,March,April,May,June,July,August,Sep,Oct,Nov,Dec,Year,ProjectId")] McashOut mcashOut)
        {
            if (ModelState.IsValid)
            {
                var DayCashOut = _context.DayCashOut.Include(d => d.Project);
                //var mcashOut = new McashOut();
                mcashOut.Jan = DayCashOut.Where(x => x.Month == 1 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.Fab = DayCashOut.Where(x => x.Month == 2 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.March = DayCashOut.Where(x => x.Month == 3 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.April = DayCashOut.Where(x => x.Month == 4 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.May = DayCashOut.Where(x => x.Month == 5 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.June = DayCashOut.Where(x => x.Month == 6 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.July = DayCashOut.Where(x => x.Month == 7 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.August = DayCashOut.Where(x => x.Month == 8 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.Sep = DayCashOut.Where(x => x.Month == 9 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.Oct = DayCashOut.Where(x => x.Month == 10 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.Nov = DayCashOut.Where(x => x.Month == 11 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                mcashOut.Dec = DayCashOut.Where(x => x.Month == 12 && x.Year == mcashOut.Year).Sum(x => x.Amount);
                _context.Add(mcashOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", mcashOut.ProjectId);
            var yearlist = _context.DayCashOut.ToList().GroupBy(m => m.Year).Select(x => x.First()).ToList();
            ViewData["MyData"] = new SelectList(yearlist, "Year", "Year");
            return View(mcashOut);
        }

        // GET: McashOuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mcashOut = await _context.McashOut.FindAsync(id);
            if (mcashOut == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", mcashOut.ProjectId);
            return View(mcashOut);
        }

        // POST: McashOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("McashOutId,Jan,Fab,March,April,May,June,July,August,Sep,Oct,Nov,Dec,Year,ProjectId")] McashOut mcashOut)
        {
            if (id != mcashOut.McashOutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mcashOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!McashOutExists(mcashOut.McashOutId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", mcashOut.ProjectId);
            return View(mcashOut);
        }

        // GET: McashOuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mcashOut = await _context.McashOut
                .Include(m => m.Project)
                .FirstOrDefaultAsync(m => m.McashOutId == id);
            if (mcashOut == null)
            {
                return NotFound();
            }

            return View(mcashOut);
        }

        // POST: McashOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mcashOut = await _context.McashOut.FindAsync(id);
            _context.McashOut.Remove(mcashOut);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool McashOutExists(int id)
        {
            return _context.McashOut.Any(e => e.McashOutId == id);
        }
    }
}
