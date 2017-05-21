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
        public ActionResult Create() {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]        
        public JsonResult Create(Order model)
        {                           
               orderService.InsertOrder2(model);
               return Json("新增成功", JsonRequestBehavior.AllowGet);              
            

        }
        public JsonResult GetCusDropListItem(KendoGridRequest request)
        {
            var CusDropListItem = orderService.GetCusDropListItem();
            return Json(CusDropListItem, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetEmpDropListItem(KendoGridRequest request)
        {
            var EmpDropListItem = orderService.GetEmpDropListItem();
            return Json(EmpDropListItem, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetShipDropListItem(KendoGridRequest request)
        {
            var ShipDropListItem = orderService.GetShipDropListItem();
            return Json(ShipDropListItem, JsonRequestBehavior.AllowGet);

        }
        ///// <summary>
        ///// 新增訂單頁面
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet] 
        //public ActionResult Create()
        //{

        //    OrderDetailViewModel viewModel= new OrderDetailViewModel();
        //    //預設增加1筆訂單明細
        //    var od = new List<OrderDetail>();
        //    od.Add(new OrderDetail() { });
        //    viewModel.OrderDetail = od;

        //    //取出下拉式選單資料並放入SelectListItem
        //    var CusDropListItem = orderService.GetCusDropListItem();
        //    var EmpDropListItem = orderService.GetEmpDropListItem();
        //    var ShipDropListItem = orderService.GetShipDropListItem();
        //    List<SelectListItem > CustId = new List<SelectListItem>();
        //    List<SelectListItem> EmpId = new List<SelectListItem>();
        //    List<SelectListItem> ShipperId = new List<SelectListItem>();
        //    foreach (var item in CusDropListItem)
        //    {
        //        CustId.Add(new SelectListItem() { Text=item.CompanyName,Value=item.CustId.ToString()});
        //    }
        //    foreach (var item in EmpDropListItem)
        //    {                
        //        EmpId.Add(new SelectListItem() { Text = item.EmpName, Value = item.EmpId.ToString()});
        //    }
        //    foreach (var item in ShipDropListItem)
        //    {
        //        ShipperId.Add(new SelectListItem() { Text = item.ShipperName, Value = item.ShipperId.ToString() });
        //    }
        //    //把剛才設置的SelectListItem放入ViewData並傳至View
        //    ViewData["CustId"] = CustId;
        //    ViewData["EmpId"] = EmpId;
        //    ViewData["ShipperId"] = ShipperId;
        //    return View(viewModel);
        //}

        ///// <summary>
        ///// 新增訂單Action
        ///// </summary>
        ///// <param name="o">整份表單</param>
        ///// <returns></returns>
        //[HttpPost]
        //public ActionResult Create(OrderDetailViewModel o){
        //    if (ModelState.IsValid) {
        //        //新增訂單
        //        orderService.InsertOrder(o);
        //        //取得剛才新增訂單的ID並新增訂單明細
        //        int orderId=orderService.getInsertOrderId();            
        //        orderService.InsertOrderDetail(o,orderId);                
        //    }          
        //    return RedirectToAction("Index");
        //}

        /// <summary>
        /// 搜尋訂單頁面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Search()
        {
            var CusDropListItem = orderService.GetCusDropListItem();
            var EmpDropListItem = orderService.GetEmpDropListItem();
            var ShipDropListItem = orderService.GetShipDropListItem();
            List<SelectListItem> CustId = new List<SelectListItem>();
            List<SelectListItem> EmpId = new List<SelectListItem>();
            List<SelectListItem> ShipperId = new List<SelectListItem>();
            foreach (var item in CusDropListItem)
            {
                CustId.Add(new SelectListItem() { Text = item.CompanyName, Value = item.CustId.ToString() });
            }
            foreach (var item in EmpDropListItem)
            {
                EmpId.Add(new SelectListItem() { Text = item.EmpName, Value = item.EmpId.ToString() });
            }
            foreach (var item in ShipDropListItem)
            {
                ShipperId.Add(new SelectListItem() { Text = item.ShipperName, Value = item.ShipperId.ToString() });
            }
            ViewData["CustId"] = CustId;
            ViewData["EmpId"] = EmpId;
            ViewData["ShipperId"] = ShipperId;
            return View();
        }

        /// <summary>
        /// 搜尋訂單Action
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(Order o)
        {           
            return View("SearchResult", orderService.SearchOrder(o));            
        }

        /// <summary>
        /// 顯示訂單詳情
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <returns></returns>
        public ActionResult Detail(int? orderId)
        {
            return View(orderService.GetOrderById(orderId));
        }

        /// <summary>
        /// 編輯訂單頁面
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int? orderId)
        {
            var CusDropListItem = orderService.GetCusDropListItem();
            var EmpDropListItem = orderService.GetEmpDropListItem();
            var ShipDropListItem = orderService.GetShipDropListItem();
            List<SelectListItem> CustId = new List<SelectListItem>();
            List<SelectListItem> EmpId = new List<SelectListItem>();
            List<SelectListItem> ShipperId = new List<SelectListItem>();
            foreach (var item in CusDropListItem)
            {
                CustId.Add(new SelectListItem() { Text = item.CompanyName, Value = item.CustId.ToString() });
            }
            foreach (var item in EmpDropListItem)
            {
                EmpId.Add(new SelectListItem() { Text = item.EmpName, Value = item.EmpId.ToString() });
            }
            foreach (var item in ShipDropListItem)
            {
                ShipperId.Add(new SelectListItem() { Text = item.ShipperName, Value = item.ShipperId.ToString() });
            }
            ViewData["CustIdItem"] = CustId;
            ViewData["EmpIdItem"] = EmpId;
            ViewData["ShipperIdItem"] = ShipperId;
            //先依訂單ID取得過往訂單，並傳資料到View
            return View(orderService.GetOrderById(orderId));
        }

        /// <summary>
        /// 編輯訂單Action
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <param name="form">表單</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int? orderId, FormCollection form)
        {
            //取得過往訂單
            var data = orderService.GetOrderById(orderId);
            //判斷前端修改的值並修改，之後傳入UpdateOrder
            if (TryUpdateModel(data, "", form.AllKeys, new string[] {  }))
            {
                orderService.UpdateOrder(data);
            }
            else {
                ModelState.AddModelError("UpdateError", "更新失敗!");
                return View(orderService);
            }
            return RedirectToAction("Index");            
        }

        /// <summary>
        /// 刪除訂單頁面
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int? orderId)
        {
            return View(orderService.GetOrderById(orderId));
        }

        /// <summary>
        /// 刪除訂單Action
        /// </summary>
        /// <param name="orderId">訂單編號</param>
        /// <param name="col">表單</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int? orderId, FormCollection col)
        {
            orderService.GetOrderById(orderId);
            if (orderService != null)
                orderService.DeleteOrder(orderId);
            return RedirectToAction("Index");
        }
    }
}
