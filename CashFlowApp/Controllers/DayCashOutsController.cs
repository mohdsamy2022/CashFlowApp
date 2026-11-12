using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CashFlowApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CashFlowApp.Controllers
{
    public class DayCashOutsController : Controller
    {
        private readonly CashFlowdbContext _context;

        public DayCashOutsController(CashFlowdbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: DayCashOuts
        public async Task<IActionResult> Index(int? year,int? month,string? SearchString,string? CurrentFilter,int? pageNumber)
        {
            var cashFlowdbContext = _context.DayCashOut.Include(d => d.Project).Where(x=>x.Project.ProjectId==StaticProject.ProjectId).OrderByDescending(x=>x.EntryDate);
            string userId = User.Identity.Name;
            if (cashFlowdbContext != null && cashFlowdbContext.Count()>0)
            {
                ViewData["delta"] = cashFlowdbContext.Select(x => x.Project).First().Name;                           //StaticProject.Name;
            }
            if (SearchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewData["CurrentFilter"] = SearchString;
            var yearlist = _context.DayCashOut.Include(d => d.Project).Where(x=>x.ProjectId==StaticProject.ProjectId).ToList().GroupBy(m => m.Year).Select(x => x.First()).ToList();
            ViewData["Year"] = new SelectList(yearlist, "Year", "Year");
            ViewData["sYear"] = year;
            var monthlist = _context.DayCashOut.Include(d => d.Project).Where(x => x.ProjectId == StaticProject.ProjectId).ToList().GroupBy(m => m.Month).Select(x => x.First()).ToList();
            ViewData["Month"] = new SelectList(monthlist, "Month", "Month");
            ViewData["sMonth"] = month;
            if (!String.IsNullOrEmpty(SearchString))
            {
                return View(await cashFlowdbContext.Where(s => s.Description.Contains(SearchString)).ToListAsync());
            }
            if(year !=null && month != null)
            {
                return View(await cashFlowdbContext.Where(s => s.Year == year && s.Month == month).ToListAsync());
            }
            int pageSize = 3;
            return View(await PaginatedList<DayCashOut>.CreateAsync(cashFlowdbContext.AsNoTracking(), pageNumber ?? 1, pageSize));
            //return View(await cashFlowdbContext.ToListAsync());
        }

        // GET: DayCashOuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayCashOut = await _context.DayCashOut
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DcashOutId == id);
            if (dayCashOut == null)
            {
                return NotFound();
            }

            return View(dayCashOut);
        }

        // GET: DayCashOuts/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects.Where(x=>x.ProjectId==StaticProject.ProjectId), "ProjectId", "Name");
            ViewData["Categories"] = new SelectList(_context.Categories, "Name", "Name");
            return View();
        }

        // POST: DayCashOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DcashOutId,Amount,Category,Description,EntryDate,Month,Year,ProjectId")] DayCashOut dayCashOut)
        {
            if (ModelState.IsValid)
            {
                dayCashOut.EntryDate = DateTime.Now;
                dayCashOut.Month = DateTime.Now.Month;
                dayCashOut.Year = DateTime.Now.Year;
                dayCashOut.AddedBy = User.Identity.Name;
                _context.Add(dayCashOut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects.Where(x => x.ProjectId == StaticProject.ProjectId), "ProjectId", "Name", dayCashOut.ProjectId);
            ViewData["Categories"] = new SelectList(_context.Categories, "Name", "Name");
            return View(dayCashOut);
        }

        // GET: DayCashOuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayCashOut = await _context.DayCashOut.FindAsync(id);
            if (dayCashOut == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", dayCashOut.ProjectId);
            ViewData["Category"] = new SelectList(_context.Categories, "Name", "Name");
            return View(dayCashOut);
        }

        // POST: DayCashOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DcashOutId,Amount,Category,Description,EntryDate,Month,Year,AddedBy,ProjectId")] DayCashOut dayCashOut)
        {
            if (id != dayCashOut.DcashOutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dayCashOut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayCashOutExists(dayCashOut.DcashOutId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", dayCashOut.ProjectId);
            ViewData["Category"] = new SelectList(_context.Categories, "Name", "Name");
            return View(dayCashOut);
        }

        // GET: DayCashOuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayCashOut = await _context.DayCashOut
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DcashOutId == id);
            if (dayCashOut == null)
            {
                return NotFound();
            }

            return View(dayCashOut);
        }

        // POST: DayCashOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayCashOut = await _context.DayCashOut.FindAsync(id);
            _context.DayCashOut.Remove(dayCashOut);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayCashOutExists(int id)
        {
            return _context.DayCashOut.Any(e => e.DcashOutId == id);
        }
    }
}
