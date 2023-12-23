using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping_System.Models;

namespace Shopping_System.Controllers
{
    public class HomeController : Controller
    {
        Online_ShoppingsDBContext db = new Online_ShoppingsDBContext();
        // GET: Home
        public ActionResult Index()
        {
            var productList = db.tbl_Product.ToList();
            return View(productList);
        }
        public ActionResult About()
        {
            return View();
        }public ActionResult Contact()
        {
            return View();
        }
    }
}