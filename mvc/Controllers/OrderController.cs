using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using KendoGridBinder;

namespace mvc.Controllers
{
    public class OrderController : Controller
    {
        private OrderService orderService = new OrderService();

        /// <summary>
        /// 首頁
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //return View(orderService.GetOrders());
            return View();
        }

        /// <summary>
        /// 取得訂單資料並以Json格式傳到首頁View
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult GetOrders(KendoGridRequest request)
        {
            var result = orderService.GetOrders();            
            return Json(new KendoGrid<Order>(request, result), JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        public ActionResult Create() {
            return View();
        }

        /// <summary>
        /// 接收訂單，並執行新增action
        /// </summary>
        /// <param name="model">整筆訂單資料</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]        
        public JsonResult Create(Order model)
        {                           
               orderService.InsertOrder2(model);
               return Json("新增成功", JsonRequestBehavior.AllowGet);            
            
        }

        /// <summary>
        /// 取得客戶下拉式選單內容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult GetCusDropListItem(KendoGridRequest request)
        {
            var CusDropListItem = orderService.GetCusDropListItem();
            return Json(CusDropListItem, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 取得員工下拉式選單內容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult GetEmpDropListItem(KendoGridRequest request)
        {
            var EmpDropListItem = orderService.GetEmpDropListItem();
            return Json(EmpDropListItem, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 取得供應商下拉式選單內容
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public JsonResult GetShipDropListItem(KendoGridRequest request)
        {
            var ShipDropListItem = orderService.GetShipDropListItem();
            return Json(ShipDropListItem, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// 接收訂單，並執行編輯action
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Edit(Order model)
        {
            //
            if (TryUpdateModel(model))
            {
                orderService.UpdateOrder2(model);
                return Json("修改成功", JsonRequestBehavior.AllowGet);
            }
            return Json("修改失敗", JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 刪除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return View();
        }

        /// <summary>
        /// 接收訂單ID，並執行刪除action
        /// </summary>
        /// <param name="orderId">訂單ID</param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Delete(int orderId)
        {
            orderService.DeleteOrder(orderId);
            return Json("刪除成功", JsonRequestBehavior.AllowGet);

        }
    }
}
