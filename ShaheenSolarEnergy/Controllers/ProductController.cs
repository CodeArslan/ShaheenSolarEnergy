using ShaheenSolarEnergy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShaheenSolarEnergy.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private ApplicationDbContext _dbContext;
        public ProductController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        // This function will return product list to show in DataTable
        public async Task<ActionResult> GetProductData()
        {
            var productList = await _dbContext.Products.AsNoTracking().ToListAsync();
            return Json(productList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ProductDetails(Product product)
        {
            try
            {
                if (product.Id == 0)
                {
                    _dbContext.Products.Add(product);
                    _dbContext.SaveChanges();
                    return Json(new { success = true, message = "Product Added Successfully." });
                }
                else
                {
                    var productInDb = _dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
                    productInDb.Name = product.Name;
                    productInDb.RetailPrice = product.RetailPrice;
                    productInDb.WholeSalePrice = product.WholeSalePrice;
                    _dbContext.SaveChanges();
                    return Json(new { success = true, message = "Product Updated Successfully." });
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }

        public ActionResult GetProductById(int id)
        {
            var product = _dbContext.Products.AsNoTracking().SingleOrDefault(p => p.Id == id);

            if (product == null)
            {
                // Handle not found case
                return Json(new { success = false, message = "Product Record Does Not Found" });
            }

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProduct(int id)
        {
            var productInDb = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            if (productInDb == null)
            {
                return Json(new { success = false, message = "Product Record Does Not Found" });
            }

            else
            {
                _dbContext.Products.Remove(productInDb);
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "Product Successfully Deleted" });
            }
        }

    }
}