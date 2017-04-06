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
        
        public void Save()
        {
            String sql = @"INSERT INTO [Sales].[OrderDetails]([OrderID],[ProductID],[Qty])
                           VALUES('','@ProductID', '2')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@ProductID", ProductID));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);                
                conn.Close();
            }
        }
       
        public void Delete()
        {

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
        public Models.Order GetOrderById(int? orderId) {
            Models.Order result = new Order();
            //result.CustId = "kuas";
            //result.CustName = "高應大";
            //result.OrderId = 001;
            //return result;

            DataTable dt = new DataTable();
            String sql = @"SELECT OrderID,CompanyName,convert(char(10),OrderDate,111) AS OrderDate,convert(char(10),ShippedDate,111) AS ShippedDate
                           FROM [Sales].[Orders] AS o                           
	                       JOIN [Sales].[Customers] AS c
	                       ON o.CustomerID=c.CustomerID
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
            
            foreach (DataRow dr in dt.Rows)
            {
                result.OrderId = Convert.ToInt32(dr["OrderId"]);
                result.CompanyName = Convert.ToString(dr["CompanyName"]);
                result.OrderDate = Convert.ToString(dr["OrderDate"]);
                result.ShippedDate = Convert.ToString(dr["ShippedDate"]);              
            }
            return result;          

        }

        /// <summary>
        /// 取得訂單列表
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders() {
            List<Order> result = new List<Order>();
            //result.Add(new Order() { OrderId = 123, CompanyName = "高應大", OrderDate = Convert.ToDateTime("2017/01/01"),ShippedDate= Convert.ToDateTime("2017/01/01") });
            DataTable dt = new DataTable();
            String sql = @"SELECT OrderID,c.CompanyName,LastName+' '+FirstName AS EmpName,ship.CompanyName AS ShipperName,convert(char(10),OrderDate,111) AS OrderDate,convert(char(10),OrderDate,111) AS RequiredDate,convert(char(10),ShippedDate,111) AS ShippedDate
                           FROM [Sales].[Orders] AS o                           
	                       JOIN [Sales].[Customers] AS c
	                       ON o.CustomerID=c.CustomerID
						   JOIN [HR].[Employees] AS e
						   ON o.EmployeeID=e.EmployeeID
						   JOIN [Production].[Suppliers] AS ship
						   ON o.[ShipperID]=ship.SupplierID";                           

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);                
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }

            result = (from i in dt.AsEnumerable() 
                      select new Order()
                      {
                          OrderId = i.Field<int>("OrderID"),
                          CompanyName = i.Field<string>("CompanyName"),
                          EmpName = i.Field<string>("EmpName"),
                          ShipperName= i.Field<string>("ShipperName"),
                          OrderDate = i.Field<string>("OrderDate"),
                          RequireDate = i.Field<string>("RequiredDate"),
                          ShippedDate = i.Field<string>("ShippedDate")

                      }).ToList<Order>();         

            return result;
        }
    }
}