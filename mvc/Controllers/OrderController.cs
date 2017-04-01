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
            ViewBag.getOrderById = orderService.GetOrderById("11069");
            ViewBag.Data = orderService.GetOrders();

            return View();
        }
        /// <summary>
        /// 新增訂單的畫面
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertOrder() {
            return View();
        }
        /// <summary>
        /// 新增訂單存檔的Action
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order) {
            ViewBag.Desc1 = "我是 ViewBag";
            ViewData["Desc2"] = "我是ViewData";
            TempData["Desc3"] = "我是TempData";
            return RedirectToAction("Index");
        }
        [HttpGet()]
        public JsonResult TestJson() {
            var result = new Models.Order() {
                CustId = "ABC",
                CustName="測試JSON"
            };
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}