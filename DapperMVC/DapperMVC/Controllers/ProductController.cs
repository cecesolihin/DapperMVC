using DapperMVC.Models;
using DapperMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperMVC.Controllers
{
    public class ProductController : Controller
    {
        private ProductRepository repo = new ProductRepository();
        //: Product
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Search(Product product)
        {
            IList<Product> listData = new List<Product>();
            try
            {
                listData = repo.ProductsSearch(product);
                ViewData["listProduct"] = listData;
            }
            catch (Exception ex)
            {

                Json ("Error : "+ex.Message.ToString());
            }
           
            return PartialView("_GridView");
        }
        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: ProductCreate
        [HttpPost]
        public JsonResult Create(Product product)
        {
            string result = string.Empty;
            try
            {

                repo.AddProduct(product);
                result = "success";
            }
            catch(Exception ex)
            {
                result = "Error : " + ex.Message.ToString();
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }

       
        // POST: Product/Edit/5
        [HttpPost]
        public JsonResult Update(Product product)
        {
            string result = string.Empty;
            try
            {

                repo.UpdateProduct(product);
                result = "success";
            }
            catch (Exception ex)
            {
                result = "Error : " + ex.Message.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProductById(string productId)
        {
            string result = string.Empty;
            Product dataProduct = new Product();
            try
            {
                dataProduct = repo.ProductById(productId); 
            }
            catch (Exception ex)
            {
                 return Json("Error : " + ex.Message.ToString());
            }
            return Json(dataProduct, JsonRequestBehavior.AllowGet);
        }

        // POST: Product/Delete/5
        public JsonResult DeleteProduct(string productId)
        {
            string result = string.Empty;
            try
            {

                repo.DeleteProduct(productId);
                result = "success";
            }
            catch (Exception ex)
            {
                result = "Error : " + ex.Message.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
