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
    public class CartController : BaseController
    {
        IRepository<Product> repo = new ProductRepository(new MobileMineContext());
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            List<Cart> carts = (List<Cart>) Session["cart"];
            ViewBag.Total = carts.Sum(t => (t.Product.Price) * (t.Quantity));
            return View(carts);
        }
        [HttpPost,ActionName("Index")]
        public ActionResult Confirm()
        {
            if (Session["username"] == null)
            {
                TempData["Error"] = "You have to Login to Purchase Product";
                return RedirectToAction("Login", "Account");
            }
            else if ((bool)Session["is_admin"])
                return Redirect("/");
            foreach (Cart cart in (List<Cart>)Session["cart"])
            {
                Buy(cart.Product.Id, cart.Quantity);
            }
            TempData["message"] = "Purchase Successful!";
            return Redirect("/Cart");
        }
        public ActionResult Remove(int id)
        {
            var cart = ((List<Cart>)Session["cart"]);
            var item = cart.Single(r => r.Product.Id == id);
            cart.Remove(item);
            return Redirect("/Cart");
        }
        [NonAction]
        public void Buy(int id, int quant)
        {
            MobileMineContext mmc = new MobileMineContext();
            Product p = repo.GetById(id);
            IRepository<User> user = new UserRepository(new MobileMineContext());
            PurchaseHistory ph = new PurchaseHistory();
            ph.BuyerId = (int)Session["user_id"];
            ph.Date = System.DateTime.Now;
            ph.Price = p.Price * quant;
            ph.ProductId = p.Id;
            p.Quantity -= quant;
            mmc.PurchaseHistories.Add(ph);
            mmc.SaveChanges();
            repo.Update(p);
            repo.Submit();
            
        }
	}
}