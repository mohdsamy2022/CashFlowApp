using CashFlowApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CashFlowApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CashFlowdbContext _context;
        public HomeController(ILogger<HomeController> logger, CashFlowdbContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {
            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name");
            ViewBag.Message = "Selected Project is: " + StaticProject.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(Projects projects)
        {
            var projectName = new Projects();
            StaticProject.ProjectId = projects.ProjectId;
            projectName = _context.Projects.Where(x => x.ProjectId == StaticProject.ProjectId).FirstOrDefault();
            StaticProject.Name = projectName.Name;
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
