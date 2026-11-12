using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CashFlowApp.Models;

namespace CashFlowApp.Controllers
{
    public class DayCashInsController : Controller
    {
        private readonly CashFlowdbContext _context;

        public DayCashInsController(CashFlowdbContext context)
        {
            _context = context;
        }

        // GET: DayCashIns
        public async Task<IActionResult> Index()
        {
            var cashFlowdbContext = _context.DayCashIn.Include(d => d.Project).Where(x => x.Project.ProjectId == StaticProject.ProjectId);
            string userId = User.Identity.Name;
            ViewData["delta"] = StaticProject.Name;
            return View(await cashFlowdbContext.ToListAsync());
        }

        // GET: DayCashIns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayCashIn = await _context.DayCashIn
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DcashInId == id);
            if (dayCashIn == null)
            {
                return NotFound();
            }

            return View(dayCashIn);
        }

        // GET: DayCashIns/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects.Where(x => x.ProjectId == StaticProject.ProjectId), "ProjectId", "Name");
            ViewData["Categories"] = new SelectList(_context.Categories, "Name", "Name");
            return View();
        }

        // POST: DayCashIns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DcashInId,Amount,Category,Description,EntryDate,Month,Year,ProjectId")] DayCashIn dayCashIn)
        {
            if (ModelState.IsValid)
            {
                dayCashIn.EntryDate = DateTime.Now;
                dayCashIn.Month = DateTime.Now.Month;
                dayCashIn.Year = DateTime.Now.Year;
                _context.Add(dayCashIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects.Where(x => x.ProjectId == StaticProject.ProjectId), "ProjectId", "Name", dayCashIn.ProjectId);
            ViewData["Categories"] = new SelectList(_context.Categories, "Name", "Name");
            return View(dayCashIn);
        }

        // GET: DayCashIns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayCashIn = await _context.DayCashIn.FindAsync(id);
            if (dayCashIn == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", dayCashIn.ProjectId);
            ViewData["Categories"] = new SelectList(_context.Categories, "Name", "Name");
            return View(dayCashIn);
        }

        // POST: DayCashIns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DcashInId,Amount,Category,Description,EntryDate,Month,Year,ProjectId")] DayCashIn dayCashIn)
        {
            if (id != dayCashIn.DcashInId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dayCashIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DayCashInExists(dayCashIn.DcashInId))
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", dayCashIn.ProjectId);
            ViewData["Categories"] = new SelectList(_context.Categories, "Name", "Name");
            return View(dayCashIn);
        }

        // GET: DayCashIns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dayCashIn = await _context.DayCashIn
                .Include(d => d.Project)
                .FirstOrDefaultAsync(m => m.DcashInId == id);
            if (dayCashIn == null)
            {
                return NotFound();
            }

            return View(dayCashIn);
        }

        // POST: DayCashIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dayCashIn = await _context.DayCashIn.FindAsync(id);
            _context.DayCashIn.Remove(dayCashIn);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DayCashInExists(int id)
        {
            return _context.DayCashIn.Any(e => e.DcashInId == id);
        }
    }
}
