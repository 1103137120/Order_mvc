using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Models
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {
        /// <summary>
        /// 新增訂單
        /// </summary>
        public void InsertOrder(Models.Order order) {

        }
        /// <summary>
        /// 刪除訂單
        /// </summary>
        public void DeleteOrder() {

        }
        /// <summary>
        /// 修改訂單
        /// </summary>
        public void UpdateOrder() {

        }
        /// <summary>
        /// 取得訂單
        /// </summary>
        /// <param name="id">訂單ID</param>
        /// <returns></returns>
        public Models.Order GetOrderById(string id) {
            Models.Order result = new Order();
            result.CusId = "test";
            result.CusName = "測試";
            return result;
        }
        /// <summary>
        /// 取得多筆訂單
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetOrders() {
            return new List<Order>();
        }
    }
}