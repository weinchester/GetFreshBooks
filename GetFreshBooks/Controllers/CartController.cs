﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetFreshBooks.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Delete(string isbn)
        {
            BusinessLogic.DeleteFromCart(isbn);
            return View("Index");
        }

        public ActionResult CheckOut()
        {
            BusinessLogic.CheckoutCart();
            return View("Checkout");
        }
    }
}