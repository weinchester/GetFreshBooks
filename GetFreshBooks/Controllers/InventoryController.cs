﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GetFreshBooks.Controllers
{
    using Models;


    public class InventoryController : Controller
    {
        Mybooks db = new Mybooks();
        // GET: Inventory
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult LoadData()
        {
            db.Configuration.LazyLoadingEnabled = false; // if your table is relational, contain foreign key
            var data = db.Books.OrderBy(a => a.BookID).ToList();
            return Json(new { data }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Save(int id)
        {
            var v = db.Books.Where(a => a.BookID == id).FirstOrDefault();
            return View(v);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var v = db.Books.Where(a => a.BookID == id).FirstOrDefault();
            if (v != null)
            {
                return View(v);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Save(Book book)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                if (book.BookID > 0)
                {
                    //Edit 
                    var v = db.Books.Where(a => a.BookID == book.BookID).FirstOrDefault();
                    if (v != null)
                    {
                        v.BookID = book.BookID;
                        v.Title = book.Title;
                        v.CategoryID = book.CategoryID;
                        v.ISBN = book.ISBN;
                        v.Author = book.Author;
                        v.Stock = book.Stock;
                        v.Price = book.Price;

                    }
                }
                else
                {
                    //Save
                    db.Books.Add(book);

                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status } };
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteBook(int id)
        {
            bool status = false;

            var v = db.Books.Where(a => a.BookID == id).FirstOrDefault();
            if (v != null)
            {
                db.Books.Remove(v);
                db.SaveChanges();
                status = true;
            }

            return new JsonResult { Data = new { status } };
        }
    }


}