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
    public class ProductController : Controller
    {
        private NimapInfotechContext db = new NimapInfotechContext();

        //
        // GET: /Product/

        public ActionResult Index()
        {
            return View(this.GetProducts(1));
        }

        [HttpPost]
        public ActionResult Index(int currentPageIndex)
        {
            return View(this.GetProducts(currentPageIndex));
        }

        private List<Product> GetProducts(int currentPage)
        {
                int maxRows = 3;
                Product productModel = new Product();
                TempData["Product"] = (from product in db.Products
                                       select product)
                            .OrderBy(product => product.ProductID)
                            .Skip((currentPage - 1) * maxRows)
                            .Take(maxRows).ToList();

                double pageCount = (double)((decimal)db.Products.Count() / Convert.ToDecimal(maxRows));
                ViewBag.PageCount = (int)Math.Ceiling(pageCount);
                ViewBag.CurrentPageIndex = currentPage;

                return TempData["Product"] as List<Product>;
        }

        //
        // GET: /Product/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "ProductName, CategoryID, CategoryName")] Product product)
        {
            product.CategoryName = db.Categories.Single(s => s.CategoryID == product.CategoryID).CategoryName;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            product.CategoryName = db.Categories.Single(s => s.CategoryID == product.CategoryID).CategoryName;
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(bool? saveChangesError = false, int id = 0)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                db.Products.Remove(product);
                db.SaveChanges();
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
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