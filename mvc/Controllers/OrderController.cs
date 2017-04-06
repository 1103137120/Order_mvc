using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;


namespace mvc.Controllers
{
    public class OrderController : Controller
    {
        private OrderService orderService = new OrderService();
        public ActionResult Index()
        {
            return View(orderService.GetOrders());
        }

        [HttpGet] 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(OrderService orderService)        {
            
            if (ModelState.IsValid) orderService.Save();            
            return RedirectToAction("Index");

        }
        public ActionResult Detail(int? orderId)
        {
            return View(orderService.GetOrderById(orderId));

        }

        [HttpGet]
        //httq://server/home/edit?name=xxx
        //提供輸入欄位及Submit鈕
        public ActionResult Edit(int? orderId)
        {
            return View(orderService.GetOrderById(orderId));
        }

        [HttpPost] //httq://server/home/edit?name=xxx 送出表單時
        public ActionResult Edit(int? orderId, FormCollection form)
        {
            //TODO: 實務上應加入更新權限檢核，在此省略
            //先讀出資料
            OrderService origPlayer = new OrderService();
            origPlayer.GetOrderById(orderId);

            //用前端傳回的資料更新Key以外的欄位
            if (
                origPlayer != null &&
                TryUpdateModel<OrderService>(
                    origPlayer,
                    //列出要更新的屬性, Name不得更換，故只有Score
                    //【補充】demo有篇TryUpdateModel介紹: http://demo.tc/Post/655
                    new string[] { "Score" }, form)
               )
                //資料無誤的話才儲存
                origPlayer.Save();
            else //更新失敗時
            {
                ModelState.AddModelError("UpdateError", "更新失敗!");
                return View(origPlayer);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        //httq://server/home/delete?name=xxx, 
        //顯示詳細資料(等同Details)，提供Submit鈕確認刪除
        public ActionResult Delete(int? orderId)
        {
            return View(orderService.GetOrderById(orderId));
        }

        [HttpPost]//httq://server/home/delete?name=xxx, 送出表單時
        public ActionResult Delete(int? orderId, FormCollection col)
        {
            orderService.GetOrderById(orderId);
            if (orderService != null)
                orderService.Delete();
            return RedirectToAction("Index");
        }

    }


    //    [HttpGet()]
    //    public JsonResult TestJson()
    //    {
    //        var result = new Models.Order()
    //        {
    //            CustId = "ABC",
    //            CustName = "測試JSON"
    //        };
    //        return this.Json(result, JsonRequestBehavior.AllowGet);
    //    }
}
