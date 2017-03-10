using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc.Models
{
    public class Order
    {
        /// <summary>
        /// 訂單編號
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 客戶編號
        /// </summary>
        public string CusId { get; set; }
        /// <summary>
        /// 客戶名稱
        /// </summary>
        public string CusName { get; set; }
    }
}