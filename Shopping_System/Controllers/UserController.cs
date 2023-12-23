using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping_System.Models;

namespace Shopping_System.Controllers
{
    public class UserController : Controller
    {
        Online_ShoppingsDBContext db = new Online_ShoppingsDBContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registration(tbl_Registration registration)
        {
            try
            {
                if (registration != null)
                {
                    db.tbl_Registration.Add(registration);
                    db.SaveChanges();
                    ModelState.Clear();
                    TempData["msg"] = "User registered successfully..";
                    return RedirectToAction("Registration");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = "User registered failed..";
                return RedirectToAction("Registration");
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_Registration login)
        {
            var userlogin = db.tbl_Registration.Where(x => x.Name == login.Name && x.Password == login.Password).Count();
            if (userlogin > 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}