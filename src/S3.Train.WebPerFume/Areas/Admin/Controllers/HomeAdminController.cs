﻿using Newtonsoft.Json;
using S3Train.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace S3.Train.WebPerFume.Areas.Admin.Controllers
{
    [Authorize(Users = "Admin")]
    public class HomeAdminController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public HomeAdminController()
        {

        }

        public HomeAdminController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}