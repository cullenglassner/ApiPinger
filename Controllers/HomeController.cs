using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ApiPinger.ViewModels;

namespace ApiPinger.Controllers
{
    public class HomeController : Controller
    {
        private ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        // public IActionResult Zillow(ListingViewModel view)
        // {
        //     if(ModelState.IsValid)
        //     {
        //         string zillowQuery = "";
        //         zillowQuery += view.Address_Line1;
        //         // TODO: Format string and send API query
        //         ViewBag.UserMessage(zillowQuery);
        //         ModelState.Clear();
        //         _logger.LogDebug($"Zillow: {zillowQuery}");
        //     }
            
        //     _logger.LogDebug($"Returning View (Zillow)");
        //     return View();
        // }

        public IActionResult Zillow()
        {
            _logger.LogDebug($"Returning View (Zillow)");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
