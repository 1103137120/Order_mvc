using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class OrderDetail
    {
        /// <summary>
        /// 訂單明細編號
        /// </summary>
        [DisplayName("訂單編號")]
        public int OrderId { get; set; }

        /// <summary>
        /// 商品編號
        /// </summary>
        [DisplayName("商品編號")]
        public int ProductId { get; set; }

        /// <summary>
        /// 單價
        /// </summary>
        [DisplayName("單價")]
        public int UnitPrice { get; set; }

        /// <summary>
        /// 數量
        /// </summary>
        [DisplayName("數量")]
        public int Qty { get; set; }

        /// <summary>
        /// 折扣
        /// </summary>
        [DisplayName("折扣")]
        public int Discount { get; set; }
    }
}