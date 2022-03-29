using ApoExTestB01.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ApoExTestB01.Controllers
{
    public class BeerController : Controller
    {
        private readonly HttpClient m_HttpClient;
        private readonly ILogger<BeerController> _logger;
        private static bool startOfLogFlag = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory">HttpClientFactory</param>
        public BeerController(IHttpClientFactory httpClientFactory, ILogger<BeerController> logger)
        {
            _logger = logger;
            this.m_HttpClient = httpClientFactory.CreateClient("PunkApiHttpClient");
        }

        /// <summary>
        /// Get results from search
        /// </summary>
        /// <param name="searchString">The string to search for</param>
        /// <param name="page">The current number of the result page</param>
        /// <returns>A list of beers</returns>
        // Index: BeerController
        public async Task<ActionResult>Index(string searchString, int page)
        {
            int beersPerPage = 10;
            List<Beer> lstBeers = null;
            List<Beer> lst10Beers = null;
            HttpResponseMessage response = null;

            // Mark start of log
            if (startOfLogFlag)
            {
                _logger.LogInformation(Environment.NewLine + "" + Environment.NewLine);
                _logger.LogInformation("###############################################");
                startOfLogFlag = false;
            }

            if (!String.IsNullOrEmpty(searchString) && searchString.Length >= 3 && searchString.Length <= 100)
            {
                ViewBag.searchString = searchString;
                searchString = searchString.Replace(" ", "_").Trim();

                if (page < 1)
                {
                    page = 1;
                }

                ViewBag.page = page;

                try
                {
                    response = await m_HttpClient.GetAsync(
                        "?beer_name=" + searchString
                    );

                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        string strResponseResult = await response.Content.ReadAsStringAsync();

                        if (strResponseResult.StartsWith("[{") && strResponseResult.EndsWith("}]"))
                        {
                            lstBeers = JsonConvert.DeserializeObject<List<Beer>>(strResponseResult);
                        }
                        else if (strResponseResult.StartsWith("{") && strResponseResult.EndsWith("}"))
                        {
                            Beer beer = JsonConvert.DeserializeObject<Beer>(strResponseResult);
                            if (beer != null)
                            {
                                lstBeers = new List<Beer>(1);
                                lstBeers.Add(beer);
                            }
                        }

                        if (lstBeers != null)
                        {
                            lst10Beers = new List<Beer>(beersPerPage);

                            for (int i = beersPerPage * (page - 1); i < page * beersPerPage; i++)
                            {
                                if (i < lstBeers.Count)
                                {
                                    lst10Beers.Add(lstBeers[i]);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            ViewBag.nrOfBeers = lstBeers.Count;
                        }
                        else
                        {
                            ViewBag.nrOfBeers = 0;
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.msgTsUSer = "Something went wrong. Please try again!";
                    _logger.LogInformation(e, e.Message);
                }
            }
            else
            {
                var images = await getHomePageImages();

                if(images != null)
                {
                    ViewBag.images = images;
                }
            }

            return View("BeerIndex", lst10Beers);
        }

        private async Task<List<string>> getHomePageImages()
        {
            var images = new List<string>();
            int nrOfBeers = 3;
            
            try
            {
                HttpResponseMessage response = null;
                List<Beer> lstBeers = null;

                do
                {
                    response = await m_HttpClient.GetAsync(
                        "/v2/beers/random"
                    );

                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        string strResponseResult = await response.Content.ReadAsStringAsync();

                        lstBeers = JsonConvert.DeserializeObject<List<Beer>>(strResponseResult);

                        if (lstBeers != null && lstBeers.Count > 0 && lstBeers[0].Image_url != null)
                        {
                            images.Add(lstBeers[0].Image_url.ToString());
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                } while (images == null || images.Count < nrOfBeers) ;

            }
            catch (Exception e)
            {
                _logger.LogInformation(e, e.Message);
            }
            
            return images;
        }
    }
}
