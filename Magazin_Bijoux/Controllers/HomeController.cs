using Magazin_Bijoux.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Magazin_Bijoux.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Magazin_Bijoux.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        private readonly ApplicationDbContext _context;
        

        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult LegalPage()
        {
            return View();
        }
        public async Task<IActionResult> Shop()
        {
            return View(await _context.Product.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
