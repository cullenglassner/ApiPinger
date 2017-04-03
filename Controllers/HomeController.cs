using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using ApiPinger.ViewModels;
using System.Net.Http;
using System.Xml;
using System.IO;

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
                        using (XmlReader reader = XmlReader.Create(new StringReader(result)))
                        {
                            _logger.LogDebug("Reader created");
                            if (reader.ReadToDescendant("text") && reader.ReadElementContentAsString() != "Request successfully processed")
                            {
                                _logger.LogDebug("No results?");
                                ViewBag.Response = $"Error, no results found.";
                            }
                            else
                            {
                                _logger.LogDebug("No error, parsing out details...");
                                if(reader.ReadToFollowing("homedetails"))
                                {
                                    string deets = reader.ReadElementContentAsString();
                                    _logger.LogDebug($"Details: {deets}");
                                    ViewBag.Details = deets;
                                }

                                if (reader.ReadToFollowing("street"))
                                {
                                    ViewBag.Street = reader.ReadElementContentAsString();
                                    _logger.LogDebug($"Street: {ViewBag.Street}");
                                }

                                if (reader.ReadToFollowing("amount"))
                                {
                                    ViewBag.Amount = reader.ReadElementContentAsString();
                                    reader.MoveToFirstAttribute();
                                    ViewBag.Currency = reader.Value;
                                }
                            }
                            
                        }
                        ViewBag.Response = result;
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
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
