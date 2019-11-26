using eCommerce.Interface;
using eCommerce.Models;
using eCommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace eCommerce.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> repo = new ProductRepository(new MobileMineContext());
        //
        // GET: /Home/
        public ActionResult Index(string currentFilter, string keyword, int? category, int? brand, int? page)
        {
            if (keyword != null)
            {
                keyword = keyword.ToLower();
                page = 1;
            }
            else
            {
                keyword = currentFilter;
            }

            ViewBag.CurrentFilter = keyword;

            var products = repo.GetAll();

            if (!String.IsNullOrEmpty(keyword))
            {
                products = products.Where(p => p.Name.ToLower().Contains(keyword)
                                       || p.Brand.Name.ToLower().Contains(keyword));
            }
            category = (category ?? 0);
            brand = (brand ?? 0);
            if (category != 0)
                products = products.Where(p => p.CategoryId == category);
            if (brand != 0)
                products = products.Where(p => p.BrandId == brand);
            
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }
	}
}