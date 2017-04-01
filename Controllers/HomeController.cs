using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ApiPinger.ViewModels;
using System.Net.Http;

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

        public async Task<IActionResult> Zillow(ListingViewModel view)
        {
            if(ModelState.IsValid)
            {
                string zillowQuery = "&address=";
                zillowQuery += view.Street_Address;
                zillowQuery += "&citystatezip=";
                if(view.Zipcode.Any())
                {
                    zillowQuery += view.Zipcode;
                }
                else if (view.City_State.Any())
                {
                    string[] citystate = view.City_State.Split(',', ' ', '-');
                    if(citystate.Length != 2) return View();
                    zillowQuery += citystate[0] + "+" + citystate[1];
                }

                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(zillowQuery);

                ViewBag.UserMessage(response.Content);
                _logger.LogDebug($"Response: {response.Content}");                
                //ViewBag.UserMessage(zillowQuery);
                //_logger.LogDebug($"Zillow: {zillowQuery}");
                ModelState.Clear();
            }
            
            _logger.LogDebug($"Returning View (Zillow) @ {DateTime.Now}");
            return View();
        }

        // public IActionResult Zillow()
        // {
        //     _logger.LogDebug($"Returning View (Zillow)");
        //     return View();
        // }

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
