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
    public class SupplierController : Controller
    {
        // GET: Supplier
        private ApplicationDbContext _dbContext;
        public SupplierController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        // This function will return supplier list to show in DataTable
        public async Task<ActionResult> GetSupplierData()
        {
            var supplierList = await _dbContext.Suppliers.AsNoTracking().ToListAsync();
            return Json(supplierList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SupplierDetails(Supplier supplier)
        {
            try
            {
                if (supplier.Id == 0)
                {
                    _dbContext.Suppliers.Add(supplier);
                    _dbContext.SaveChanges();
                    return Json(new { success = true, message = "Supplier Added Successfully." });
                }
                else
                {
                    var supplierInDb = _dbContext.Suppliers.FirstOrDefault(s => s.Id == supplier.Id);
                    supplierInDb.Name = supplier.Name;
                    supplierInDb.Address = supplier.Address;
                    supplierInDb.Phone = supplier.Phone;
                    supplierInDb.Telephone = supplier.Telephone;
                    _dbContext.SaveChanges();
                    return Json(new { success = true, message = "Supplier Updated Successfully." });
                }
            }
            catch (Exception)
            {
                // Log the exception or handle it as needed
                return Json(new { success = false, message = "An error occurred while processing your request." });
            }
        }

        public ActionResult GetSupplierById(int id)
        {
            var supplier = _dbContext.Suppliers.AsNoTracking().SingleOrDefault(s => s.Id == id);

            if (supplier == null)
            {
                // Handle not found case
                return Json(new { success = false, message = "Supplier Record Does Not Found" });
            }

            return Json(supplier, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteSupplier(int id)
        {
            var supplierInDb = _dbContext.Suppliers.SingleOrDefault(s => s.Id == id);
            if (supplierInDb == null)
            {
                return Json(new { success = false, message = "Supplier Record Does Not Found" });
            }

            else
            {
                _dbContext.Suppliers.Remove(supplierInDb);
                _dbContext.SaveChanges();
                return Json(new { success = true, message = "Supplier Successfully Deleted" });
            }
        }

        // This function is used to check duplicate name for supplier
        [HttpPost]
        public JsonResult IsSupplierNameAvailable(string name, int? id, bool isUpdate)
        {
            bool isNameAvailable;

            if (isUpdate && id.HasValue)
            {
                // For update, exclude the current supplier name with the specified ID
                isNameAvailable = !_dbContext.Suppliers.AsNoTracking().Any(x => x.Name == name && x.Id != id.Value);
            }
            else
            {
                // For add operation or if the ID is not provided, check for duplicates without exclusion
                isNameAvailable = !_dbContext.Suppliers.AsNoTracking().Any(x => x.Name == name);
            }

            return Json(isNameAvailable);
        }

    }
}