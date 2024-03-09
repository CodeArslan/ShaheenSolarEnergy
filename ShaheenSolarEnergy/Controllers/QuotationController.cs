using ShaheenSolarEnergy.Models;
using ShaheenSolarEnergy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShaheenSolarEnergy.Controllers
{
    public class QuotationController : Controller
    {
        // GET: Quotation
        private ApplicationDbContext _dbContext;
        public QuotationController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index(int? id)
      {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            QuotationOrderViewModel viewModel;

            if (id != null)
            {
                // Retrieve existing order from the database based on the id
                var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
                if (existingOrder == null)
                {
                    // Handle the case where the order with the given id does not exist
                    // For example, you can redirect to a different action or show an error message
                    return RedirectToAction("NotFound");
                }
                var existingOrderQuotations = _dbContext.Quotations.Where(q => q.Order.OrderNumber == existingOrder.OrderNumber).ToList();

                // Populate the view model with existing order details and its associated quotations
                viewModel = new QuotationOrderViewModel
                {
                    Products = _dbContext.Products.ToList(),
                    Orders = existingOrder,
                    Quotations = existingOrderQuotations
                };
            }
            else
            {
                // If id is null, create a new order with the next order number
                int lastOrderNumber = GetLastOrderNumberFromDatabase();
                int nextOrderNumber = lastOrderNumber + 1;
                viewModel = new QuotationOrderViewModel
                {
                    Products = _dbContext.Products.ToList(),
                    Orders = new Order { OrderNumber = nextOrderNumber },
                    Quotations = new List<Quotation>() // Initialize with an empty list for new order
                };
            }

            return View(viewModel);
        }
        //public ActionResult GetQuotations(int id)
        //{

        //    var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
        //    if (existingOrder == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var existingOrderQuotations = (from quotation in _dbContext.Quotations
        //                                   join product in _dbContext.Products on quotation.ProductId equals product.Id
        //                                   where quotation.OrderId == existingOrder.OrderNumber
        //                                   select new
        //                                   {
        //                                       ProductName = product.Name,
        //                                       quotation.UnitPrice,
        //                                       quotation.Quantity,
        //                                       quotation.TotalCost
        //                                   }).ToList();

        //    return Json(existingOrderQuotations, JsonRequestBehavior.AllowGet);
        //}
        [HttpGet]
        public ActionResult GetQuotations(int orderNumber)
        {
            var quotations = (from q in _dbContext.Quotations
                              join o in _dbContext.Orders on q.OrderId equals o.Id
                              where o.OrderNumber == orderNumber
                              select new
                              {
                                  q.Id,
                                  ProductName = q.Product.Name,
                                  q.UnitPrice,
                                  q.Quantity,
                                  Cost = q.TotalCost
                              }).ToList();

            return Json(quotations, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetOrderDetails(int id)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return HttpNotFound(); 
            }

            var orderDetails = new
            {
                OrderNumber = order.OrderNumber,
                OrderStatus = order.OrderStatus,
                OrderDate = order.OrderDate.ToString("dd MMM, yyyy"),
                CustomerName = order.CustomerName,
                CustomerAddress = order.CustomerAddress
            };

            return Json(orderDetails, JsonRequestBehavior.AllowGet);
        }
        public int GetLastOrderNumberFromDatabase()
        {
                var lastOrder =_dbContext.Orders.OrderByDescending(o => o.OrderNumber).FirstOrDefault();
                if (lastOrder != null)
                {
                    return lastOrder.OrderNumber;
                }
                else
                {
                    return 0;
                }
        }
        [HttpGet]
        public ActionResult GetProductById(int productId)
        {
            var product = _dbContext.Products.Where(p=>p.Id==productId).FirstOrDefault();

            if (product != null)
            {
                return Json(product, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return HttpNotFound();
            }
        }
        [HttpPost]
        public ActionResult SaveQuotation(List<Quotation> quotationItems)
        {
            if (ModelState.IsValid)
            {
                int orderNumber = 0;
                foreach (var item in quotationItems)
                {
                    // Check if an order with the specified OrderId exists
                    var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.OrderNumber == item.OrderId);

                    // If order does not exist, return error message
                    if (existingOrder == null)
                    {
                        return Json(new { success = false, message = "Order details need to be saved first." });
                    }

                    var existingQuotation = _dbContext.Quotations.FirstOrDefault(q => q.ProductId == item.ProductId && q.OrderId == existingOrder.Id);

                    if (existingQuotation != null)
                    {
                        // Update the existing quotation item
                        existingQuotation.UnitPrice = item.UnitPrice;
                        existingQuotation.Quantity = item.Quantity;
                        existingQuotation.TotalCost = item.TotalCost;
                    }
                    else
                    {
                        // Create a new quotation item
                        var quotationItem = new Quotation
                        {
                            ProductId = item.ProductId,
                            UnitPrice = item.UnitPrice,
                            Quantity = item.Quantity,
                            TotalCost = item.TotalCost,
                            OrderId = existingOrder.Id
                        };
                        _dbContext.Quotations.Add(quotationItem);
                    }
                    orderNumber = existingOrder.OrderNumber;
                }
               
                _dbContext.SaveChanges();

                return Json(new { success = true, message = "Quotation saved successfully", orderNumber = orderNumber });
            }
            else
            {
                // If ModelState is not valid, return error message
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return Json(new { success = false, errors = errors });
            }
        }
        [HttpPost]
        public ActionResult SaveOrder(QuotationOrderViewModel Order)
        {
            if (ModelState.IsValid)
            {
                // Check if the order already exists in the database
                var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.Id == Order.Orders.Id);

                if (existingOrder != null)
                {
                    // Update existing order details
                    existingOrder.OrderNumber = Order.Orders.OrderNumber;
                    existingOrder.OrderStatus = Order.Orders.OrderStatus;
                    existingOrder.OrderDate = Order.Orders.OrderDate;
                    existingOrder.BillTo = Order.Orders.BillTo;
                    existingOrder.CustomerAddress = Order.Orders.CustomerAddress;
                    existingOrder.CustomerName = Order.Orders.CustomerName;
                    _dbContext.SaveChanges();

                    return Json(new { success = true, message = "Order Details Updated Successfully", orderId = existingOrder.Id });
                }
                else
                {
                    // Create a new order
                    var newOrder = new Order
                    {
                        OrderNumber = Order.Orders.OrderNumber,
                        OrderStatus = Order.Orders.OrderStatus,
                        OrderDate = Order.Orders.OrderDate,
                        BillTo = Order.Orders.BillTo,
                        CustomerAddress = Order.Orders.CustomerAddress,
                        CustomerName = Order.Orders.CustomerName
                    };

                    _dbContext.Orders.Add(newOrder);
                    _dbContext.SaveChanges();

                    return Json(new { success = true, message = "Order Details Saved Successfully", orderId = newOrder.Id });
                }

               
            }
            else
            {
                return Json(new { success = false, message = "An Error Occured While Saving Order Details" });
            }
        }

        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                var order = _dbContext.Orders.Find(id);
                if (order == null)
                {
                    return Json(new { success = false, message = "Order not found." });
                }

                var relatedQuotations = _dbContext.Quotations.Where(q => q.OrderId == id).ToList();
                _dbContext.Quotations.RemoveRange(relatedQuotations);

                _dbContext.Orders.Remove(order);
                _dbContext.SaveChanges();

                TempData["SuccessMessage"] = "Order deleted successfully.";
                return Json(new { success = true, message = "Order Deleted" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while deleting the order." });
            }
        }

    }
}