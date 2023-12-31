﻿using Cibertec.Shopping.WEB.Models;
using Cibertec.Shopping.WEB.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cibertec.Shopping.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var userResult = TempData.Get<UserResultViewModel>("UserResult");
            ViewData["UserResult"] = userResult;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}