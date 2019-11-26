using eCommerce.Interface;
using eCommerce.Models;
using eCommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class AdminController : BaseController
    {
        IRepository<PurchaseHistory> historyRepo = new PurchaseHistoryRepository(new MobileMineContext());
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            ViewBag.Monthly = historyRepo.GetAll().Where(t => t.Date.Month == System.DateTime.Now.Month && t.Date.Year == System.DateTime.Now.Year).Sum(t => t.Price);
            ViewBag.Daily = historyRepo.GetAll().Where(t => t.Date.Date == System.DateTime.Now.Date).Sum(t => t.Price);
            ViewBag.Product = historyRepo.GetAll().Select(p => p.Product.Brand.Name).First()+ " " +historyRepo.GetAll().Select(p => p.Product.Name).First();
            return View();
        }
	}
}