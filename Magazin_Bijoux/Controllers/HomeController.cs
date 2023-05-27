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
using Microsoft.Extensions.Localization;

namespace Magazin_Bijoux.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _logger = logger;
            _context = context;
            _sharedLocalizer = sharedLocalizer;
        }
        private readonly ApplicationDbContext _context;

       
        public async Task<IActionResult> Index()
        {
            var productContext = await _context.Product.ToListAsync();
            foreach (var item in productContext)
            {
                item.name = _sharedLocalizer[item.name];
                item.details = _sharedLocalizer[item.details];
                item.color = _sharedLocalizer[item.color];
            }
            return View(productContext);
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
            var productContext = await _context.Product.ToListAsync();
            foreach (var item in productContext)
            {
                item.name = _sharedLocalizer[item.name];
                item.details = _sharedLocalizer[item.details];
                item.color = _sharedLocalizer[item.color];
            }
            return View(productContext);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
