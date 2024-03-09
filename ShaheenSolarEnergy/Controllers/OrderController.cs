using ShaheenSolarEnergy.Models;
using ShaheenSolarEnergy.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShaheenSolarEnergy.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private ApplicationDbContext _dbContext;
        public OrderController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public async Task<ActionResult> Index()
        {
            var orders = await _dbContext.Orders.ToListAsync();
            return View(orders);
        }
        [HttpGet]
        public ActionResult FilteredOrders(string status, string orderNumber)
        {
            IQueryable<Order> filteredOrders = _dbContext.Orders;

            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                filteredOrders = filteredOrders.Where(o => o.OrderStatus == status);
            }

            if (!string.IsNullOrEmpty(orderNumber))
            {
                filteredOrders = filteredOrders.Where(o => o.OrderNumber.ToString().Contains(orderNumber));
            }

            var result = filteredOrders.ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}