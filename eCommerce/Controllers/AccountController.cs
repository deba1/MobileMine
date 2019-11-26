using eCommerce.Interface;
using eCommerce.Models;
using eCommerce.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        IRepository<User> repo = new UserRepository(new MobileMineContext());
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (Session["username"] != null)
                return Redirect("/");
            User user = repo.GetAll().Where(u=>u.Username==username && u.Password==password).FirstOrDefault();

            if (user != null)
            {
                Session["user_id"] = user.Id;
                Session["username"] = user.Username;
                Session["is_admin"] = user.IsAdmin;
                Session["cart"] = new List<Cart>();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Username/Password is Incorrect";
                return RedirectToAction("Login");
            }
        }
        [HttpGet]
        public ActionResult Register()
        {
            if (Session["username"] != null)
                return Redirect("/");
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repo.Insert(user);
                    repo.Submit();
                    TempData["New"] = "Account Successfully Created. Please proceed to login.";
                    return RedirectToAction("Login");
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = "Username Already Exist!";
            }
            return View(user);
        }
        [HttpGet]
        public ActionResult Manage()
        {
            return View(repo.GetById((int)Session["user_id"]));
        }

        [HttpPost]
        public ActionResult Manage(User user)
        {
            if (ModelState.IsValid)
            {
                repo.Update(user);
                repo.Submit();
                TempData["message"] = "Profile Updated";
                return RedirectToAction("Manage");
            }
            return View(user);
        }
        
        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }

	}
}