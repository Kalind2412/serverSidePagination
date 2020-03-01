using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NimapInfotechDemo.Models;
using NimapInfotechDemo.DAL;

namespace NimapInfotechDemo.Controllers
{
    public class CategoryController : Controller
    {
        private NimapInfotechContext db = new NimapInfotechContext();

        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "CategoryName")] Category category)
        {   
            try
            {
                if (ModelState.IsValid)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
           catch (DataException)
           {
              ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
           }

            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(bool? saveChangesError=false,int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Category category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}