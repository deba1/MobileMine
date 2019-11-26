using eCommerce.Interface;
using eCommerce.Models;
using eCommerce.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class AjaxController : Controller
    {
        IRepository<Category> repoCategory = new CategoryRepository(new MobileMineContext());
        IRepository<Brand> repoBrand = new BrandRepository(new MobileMineContext());
        IRepository<Product> repoProduct = new ProductRepository(new MobileMineContext());

        //
        // GET: /Ajax/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Categories()
        {
            return Json(repoCategory.GetAll().Select(c=>c.Name),JsonRequestBehavior.AllowGet);
        }

        public ActionResult Brands()
        {
            return Json(repoBrand.GetAll().Select(b => b.Name), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Products()
        {
            var list = repoProduct.GetAll().Select(p => p.Name);
            list = list.Concat(repoBrand.GetAll().Select(b => b.Name));
            return Json(list, JsonRequestBehavior.AllowGet);
        }
	}
}