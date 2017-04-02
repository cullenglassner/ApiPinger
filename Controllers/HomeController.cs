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

        public async Task<IActionResult> Zillow(ListingViewModel model)
        {
            if(ModelState.IsValid)
            {
                string zillowQuery = "http://www.zillow.com/webservice/GetSearchResults.htm?zws-id=";
                zillowQuery += "X1-ZWz19bi4i5houj_5cfwc";
                zillowQuery += "&address=";
                zillowQuery += model.Address.Replace(' ', '+');
                zillowQuery += "&citystatezip=";
                if(model.Zipcode.Any())
                {
                    zillowQuery += model.Zipcode;
                }
                else if (model.CityState.Any())
                {
                    string[] citystate = model.CityState.Split(',', ' ', '-');
                    if(citystate.Length != 2) return View();
                    zillowQuery += citystate[0] + "+" + citystate[1];
                }

                _logger.LogDebug($"Zillow Query String: {zillowQuery}");

                try{
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(zillowQuery);
                    response.EnsureSuccessStatusCode();
                    HttpContent content = response.Content;
                    string result = await content.ReadAsStringAsync();
                    if(result != null)
                    {
                        _logger.LogDebug($"Response: {result}");
                        ViewBag.MyString = result;
                    }
                }
                catch (Exception e)
                {
                    ViewBag.UserMessage(e.Message);
                }
                ModelState.Clear();
            }
            
            _logger.LogDebug($"Returning View (Zillow) @ {DateTime.Now}");
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
