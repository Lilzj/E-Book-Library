﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Library.Controllers
{
    public class Account : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}