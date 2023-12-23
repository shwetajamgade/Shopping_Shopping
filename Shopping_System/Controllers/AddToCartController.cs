using Shopping_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Shopping_System.Controllers
{
    public class AddToCartController : Controller
    {
        Online_ShoppingsDBContext db = new Online_ShoppingsDBContext();
        // GET: AddToCart
        public ActionResult Index()
        {
            return View();
        }

        //AddToCart() Add products into the cart
        public ActionResult AddToCart(int productId)
        {
            if (Session["Cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                //Cart item = new Cart();
                //item.Product = db.tbl_Product.Find(ProductID);
                //item.ProductQuantity = 1;
                //cart.Add(item);
                cart.Add(new Cart()
                {
                    Product = db.tbl_Product.Find(productId),
                    Quantity = 1
                });
                Session["Cart"] = cart;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["Cart"];
                int index = IsInCart(productId);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Cart()
                    {
                        Product = db.tbl_Product.Find(productId),
                        Quantity = 1
                    });
                }
                Session["Cart"] = cart;
            }
            return RedirectToAction("Index");
        }

        //RemoveFromCart() remove From Cart
        public ActionResult RemoveFromCart(int productId)
        {
            List<Cart> cart = (List<Cart>)Session["Cart"];
            int index = IsInCart(productId);
            cart.RemoveAt(index);
            Session["Cart"] = cart;
            return RedirectToAction("Index");
        }

        // IsInCart() check the products are present in cart or not
        public int IsInCart(int productId)
        {
            List<Cart> cart = (List<Cart>)Session["Cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.ProductID == productId)
                {
                    return i;
                }
            }
            return -1;
            
        }
    }
}