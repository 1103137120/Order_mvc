using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class OrderController : Controller
    {
       /// <summary>
       /// 訂單管理首頁
       /// </summary>
       /// <returns></returns>
        public ActionResult Index()
        {
            Models.OrderService orderService = new Models.OrderService();
            var order = orderService.GetOrderById("11");
            ViewBag.CusId = order.CusId;
            ViewBag.CusName = order.CusName;
            return View();
        }
    }
}