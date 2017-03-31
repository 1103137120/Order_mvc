using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace mvc.Models
{
    /// <summary>
    /// 訂單服務
    /// </summary>
    public class OrderService
    {
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }

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
        /// <param name="orderId">訂單ID</param>
        /// <returns></returns>
        public Models.Order GetOrderById(string orderId) {
            Models.Order result = new Order();
            //result.CustId = "kuas";
            //result.CustName = "高應大";
            //result.OrderId = 001;
            //return result;

            DataTable dt = new DataTable();
            String sql = @"SELECT o.[OrderID],o.[OrderDate],o.[RequiredDate],e.[FirstName],e.[LastName],p.[ProductName],p.[UnitPrice],od.[Qty],p.[UnitPrice]*od.[Qty] AS Subtotal
                            FROM [Sales].[Orders] AS o 
                            JOIN [Sales].[OrderDetails] AS od 
	                            ON o.[OrderID]= od.[OrderID]
                            JOIN [Production].[Products] AS p 
	                            ON p.[ProductID] = od.[ProductID]
                            JOIN [HR].[Employees] AS e 
	                            ON e.[EmployeeID]=o.[EmployeeID]
                            WHERE o.[OrderID]=@OrderId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {

                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", orderId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);                
                conn.Close();
                
                
            }

            return null;


        }

        /// <summary>
        /// 取得多筆訂單
        /// </summary>
        /// <returns></returns>
        public List<Models.Order> GetOrders() {
            List<Models.Order> result = new List<Order>();
            result.Add(new Order() { CustId = "kuas", CustName = "高應大", OrderId = 001 });
            result.Add(new Order() { CustId = "nkfust", CustName = "高第一", OrderId = 002 });
            return result;
        }
    }
}