using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shopping_System.Models;

namespace Shopping_System.Controllers
{
    public class ProductController : Controller
    {
        Online_ShoppingsDBContext db= new Online_ShoppingsDBContext();
        // GET: Product
        public ActionResult Index()
        {
            //var produtList = db.tbl_Product.ToList();
            return View();
        }

        // to fetch data from db to show product list
        [HttpGet]
        public ActionResult CreateProducts() 
        { 
            return View(new tbl_Product());  
        }

        //To add data to db
        [HttpPost]
        public ActionResult CreateProducts(tbl_Product createProducts, HttpPostedFileBase ImageUpload)
        {
            
            try
            {
                // upload image
                string extension = Path.GetExtension(ImageUpload.FileName);
                if (extension.Equals(".jpg") || extension.Equals(".png"))
                {
                    string filename = "IMG-" + DateTime.Now.ToString("yyyymmddhhmmssffff") + extension;
                    string savepath = Server.MapPath("~/UploadeImages/");
                    ImageUpload.SaveAs(savepath + filename);
                    createProducts.ProductImage = filename;

                    //add products to db
                    db.tbl_Product.Add(createProducts);
                    db.SaveChanges();
                    ModelState.Clear();
                }
                else
                {
                    return Content("<h5>you can upload only jpg & png files...</h5>");
                }
                return RedirectToAction("ProductList");
            }
            catch (Exception)
            {
                return View();
            }           
            
        }
        // to show product list
        public ActionResult ProductList()
        {
            var productlist = db.tbl_Product.ToList();
            return View(productlist);
        }

        // get data from create form to edit products
        [HttpGet]
        public ActionResult EditProduct(int id) 
        {
            var findId = db.tbl_Product.Find(id);
            return View(findId);
        }
        // edit data from create form and add data to db
        [HttpPost]
        public ActionResult EditProduct(tbl_Product editProduct, HttpPostedFileBase ImageUpload)
        {
            try
            {
                // code for update image
                string filename = "";
                if (ImageUpload != null)
                {
                    string extension = Path.GetExtension(ImageUpload.FileName);
                    if (extension.Equals(".jpg") || extension.Equals(".png"))
                    {
                        filename = "IMG-" + DateTime.Now.ToString("yyyymmddhhmmssffff") + extension;
                        string savepath = Server.MapPath("~/UploadeImages/");
                        ImageUpload.SaveAs(savepath + filename);
                    }

                }
                db.Entry(editProduct).State = System.Data.Entity.EntityState.Modified;
                if (!filename.Equals(""))
                {
                    editProduct.ProductImage = filename;
                }

                db.SaveChanges();
                return RedirectToAction("ProductList");
            }
            catch (Exception)
            {

                throw;
            }
           // return View();
        }

        //to delete products from the productlist
        public ActionResult DeleteProduct(int id)
        {
            var deleteproduct = db.tbl_Product.Find(id);
            db.tbl_Product.Remove(deleteproduct);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
    }
}