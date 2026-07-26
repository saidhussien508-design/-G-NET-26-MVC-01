using GemMangement.Models;
using GemMangement_AL_.Servicess.Interfases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GemMangement.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnaiyticsServices _anaiyticsServices;

        public HomeController(ILogger<HomeController> logger
            ,IAnaiyticsServices anaiyticsServices)
        {
            _logger = logger;
            _anaiyticsServices = anaiyticsServices;
        }

        public async Task<IActionResult> Index(CancellationToken ct)
        {
            var result = await _anaiyticsServices.GetDataasync(ct);
            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //    public IActionResult Error()
        //    {
        //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //    }
        //}
    }
}
