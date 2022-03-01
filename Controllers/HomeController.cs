using ApoExTestB01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApoExTestB01.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;

        public RedirectToRouteResult Index()
        {
            return RedirectToRoute(new { controller = "Beer", action = "Index" });
        }
    }
}
