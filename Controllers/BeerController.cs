using ApoExTestB01.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApoExTestB01.Controllers
{
    public class BeerController : Controller
    {
        private HttpClient m_HttpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory">HttpClientFactory</param>
        public BeerController(IHttpClientFactory httpClientFactory)
        {
            this.m_HttpClient = httpClientFactory.CreateClient("PunkApiHttpClient");
        }

        /// <summary>
        /// Get results from search
        /// </summary>
        /// <param name="searchString">The string that the user used</param>
        /// <param name="page">The current result page</param>
        /// <returns></returns>
        // GET: BeerController
        public async Task<ActionResult>Index(string searchString, int page)
        {
            int beersPerPage = 10;
            List<Beer> lstBeers = null;
            List<Beer> lst10Beers = null;
            HttpResponseMessage response = null;

            if (!String.IsNullOrEmpty(searchString) && searchString.Length >= 3)
            {
                searchString = searchString.Replace(" ", "_");
                ViewBag.searchString = searchString;

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
                                if (i < lstBeers.Count())
                                {
                                    lst10Beers.Add(lstBeers[i]);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            ViewBag.nrOfBeers = lstBeers.Count();
                        }
                        else
                        {
                            ViewBag.nrOfBeers = 0;
                        }
                    }
                    else
                    {
                        throw new WebException();
                    }
                }
                catch (Exception e)
                {
                    // Console.WriteLine(e.Message, e);
                    ViewBag.msgTsUSer = "Something went wrong. Please try again!";
                }
            }

            return View("BeerIndex", lst10Beers);
        }
         }
}
