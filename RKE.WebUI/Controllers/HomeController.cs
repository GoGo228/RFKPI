﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RKE.BL.Abstract;
using RKE.UIModels.RozkladModelForStudents;

namespace RKE.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
       
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Details/5
    }
}
