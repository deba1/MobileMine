using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eCommerce.Models;

namespace eCommerce.Controllers
{
    public class HistoryController : BaseController
    {
        private MobileMineContext db = new MobileMineContext();

        // GET: /History/
        public ActionResult Index()
        {
            var purchasehistories = db.PurchaseHistories.Include(p => p.Product).Include(p => p.User);
            if (Session["is_admin"] != null && !(bool)Session["is_admin"])
            {
                var username = Session["username"].ToString();
                purchasehistories = purchasehistories.Where(u => u.User.Username == username);
            }
                
            return View(purchasehistories.ToList());
        }

        // GET: /History/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseHistory purchasehistory = db.PurchaseHistories.Find(id);
            if (purchasehistory == null)
            {
                return HttpNotFound();
            }
            return View(purchasehistory);
        }

        // GET: /History/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username");
            return View();
        }

        // POST: /History/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,ProductId,BuyerId,Date,Price")] PurchaseHistory purchasehistory)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseHistories.Add(purchasehistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", purchasehistory.ProductId);
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username", purchasehistory.BuyerId);
            return View(purchasehistory);
        }

        // GET: /History/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseHistory purchasehistory = db.PurchaseHistories.Find(id);
            if (purchasehistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", purchasehistory.ProductId);
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username", purchasehistory.BuyerId);
            return View(purchasehistory);
        }

        // POST: /History/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,ProductId,BuyerId,Date,Price")] PurchaseHistory purchasehistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchasehistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", purchasehistory.ProductId);
            ViewBag.BuyerId = new SelectList(db.Users, "Id", "Username", purchasehistory.BuyerId);
            return View(purchasehistory);
        }

        // GET: /History/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseHistory purchasehistory = db.PurchaseHistories.Find(id);
            if (purchasehistory == null)
            {
                return HttpNotFound();
            }
            return View(purchasehistory);
        }

        // POST: /History/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseHistory purchasehistory = db.PurchaseHistories.Find(id);
            db.PurchaseHistories.Remove(purchasehistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
