﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LocalSpirits.WebMVC.Controllers
{
    public class BreweryController : Controller
    {
        // GET: Brewery
        public ActionResult Index()
        {
            return View();
        }
    }
}