using Cinemy.Core.Contracts;
using Cinemy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cinemy.WebUI.Controllers
{
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService OrderService)
        {
            this.orderService = OrderService;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            return View(orders);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateOrder(string Id)
        {
            ViewBag.StatusList = new List<string>()
            {
                "Order Created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = orderService.GetOrder(Id);
            return View(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateOrder(Order updatedOrder, string Id)
        {
            Order order = orderService.GetOrder(Id);
            order.OrderStatus = updatedOrder.OrderStatus;
            orderService.UpdateOrder(order);
            return RedirectToAction("Index");
        }

        public ActionResult Details(string Id)
        {
            Order order = orderService.GetOrder(Id);
            return View(order);
        }
    }
}