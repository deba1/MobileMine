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
    public class PurchaseController : Controller
    {
        IRepository<Product> repo = new ProductRepository(new MobileMineContext());
        
        //
        // GET: /Purchase/
        public ActionResult Index(int id)
        {
            return View(repo.GetById(id));
        }
        [HttpPost]
        [ActionName("Index")]
        public ActionResult Purchase(int id, int quant)
        {
            if (Session["username"] == null)
            {
                TempData["Error"] = "You have to Login to Purchase Product";
                return RedirectToAction("Login", "Account");
            }
            else if ((bool)Session["is_admin"])
                return Redirect("/");
            Product p = repo.GetById(id);
            if (p.Quantity<=1)
            {
                TempData["message"] = "Insufficient Item!";
                return Redirect("/Purchase/Index/"+id);
            }
            IRepository<User> user = new UserRepository(new MobileMineContext());
            PurchaseHistory ph = new PurchaseHistory();
            ph.BuyerId = (int)Session["user_id"];
            ph.Date = System.DateTime.Now;
            ph.Price = p.Price*quant;
            ph.ProductId = p.Id;
            p.Quantity-=quant;
            MobileMineContext mmc = new MobileMineContext();
            mmc.PurchaseHistories.Add(ph);
            mmc.SaveChanges();
            repo.Update(p);
            repo.Submit();
            TempData["message"] = "Purchase Successful!";
            return Redirect("/Purchase/Index/" + id);
        }
        public ActionResult AddToCart(int id, int quant)
        {
            if (Session["username"] == null)
            {
                TempData["Error"] = "You have to Login to Purchase Product";
                return RedirectToAction("Login", "Account");
            }
            else if ((bool)Session["is_admin"])
                return Redirect("/");
            Product p = repo.GetById(id);
            if (p.Quantity <= 1)
            {
                TempData["message"] = "Insufficient Item!";
                return Redirect("/Purchase/Index/" + id);
            }
            Cart item = new Cart()
            {
                Product = p,
                Quantity = quant,
            };
            List<Cart> cart = new List<Cart>();
            cart.Add(item);
            if ((IEnumerable<Cart>)Session["cart"]!=null)
                cart = cart.Concat((IEnumerable<Cart>)Session["cart"]).ToList();
            Session["cart"] = cart;

            TempData["message"] = "Added to Cart";
            return Redirect("/Purchase/Index/" + id);
        }
	}
}