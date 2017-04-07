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
        /// <summary>
        /// 連線
        /// </summary>
        /// <returns></returns>
        private string GetDBConnectionString()
        {
            return
                System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
        }
        /// <summary>
        /// 新增訂單
        /// </summary>
        /// <param name="order"></param>
        public void InsertOrder(Models.Order order)
        {
            Models.Order result = new Order();
            DataTable dt = new DataTable();
            String sql = @"INSERT INTO [Sales].[Orders]([CustomerID],[EmployeeID],[OrderDate],[RequiredDate],[ShippedDate],[Freight],[ShipperID],[ShipName],[ShipAddress],[ShipCity],[ShipRegion],[ShipPostalCode],[ShipCountry])
                           VALUES (@CustomerID,@EmployeeID,@OrderDate,@RequiredDate,@ShippedDate,@Freight,@ShipperID,@ShipName,@ShipAddress,@ShipCity,@ShipRegion,@ShipPostalCode,@ShipCountry)";
            //String sql = @"INSERT INTO [Sales].[Orders]([CustomerID],[EmployeeID],[OrderDate],[RequiredDate],[ShippedDate],[Freight],[ShipperID],[ShipName],[ShipAddress],[ShipCity],[ShipRegion],[ShipPostalCode],[ShipCountry])
            //                VALUES (1,5,2017-02-03,2017-02-03,2017-02-03,3,3,3,3,3,1,1,'TW')";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@CustomerID",order.CustId));
                cmd.Parameters.Add(new SqlParameter("@EmployeeID", order.EmpId));
                cmd.Parameters.Add(new SqlParameter("@OrderDate", order.OrderDate));
                cmd.Parameters.Add(new SqlParameter("@RequiredDate", order.RequireDate));
                cmd.Parameters.Add(new SqlParameter("@ShippedDate", order.ShippedDate));
                cmd.Parameters.Add(new SqlParameter("@Freight", order.Freight));
                cmd.Parameters.Add(new SqlParameter("@ShipperID", order.ShipperId));
                cmd.Parameters.Add(new SqlParameter("@ShipName", order.ShipName));
                cmd.Parameters.Add(new SqlParameter("@ShipAddress", order.ShipAddress));
                cmd.Parameters.Add(new SqlParameter("@ShipCity", order.ShipCity));
                cmd.Parameters.Add(new SqlParameter("@ShipRegion", order.ShipRegion));
                cmd.Parameters.Add(new SqlParameter("@ShipPostalCode", order.ShipPostalCode));
                cmd.Parameters.Add(new SqlParameter("@ShipCountry", order.ShipCountry));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }          
        }
        public void InsertOrderDetail(Models.Order order)
        {
        }

        /// <summary>
        /// 刪除訂單
        /// </summary>
        public void DeleteOrder(int? orderId) {
            DataTable dt = new DataTable();
            String sql = @"DELETE FROM [Sales].[Orders]
                           WHERE [OrderID]=@OrderId";

            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@OrderId", orderId));
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
        }
        /// <summary>
        /// 修改訂單
        /// </summary>
        public void UpdateOrder() {

        }
    /// <summary>
    /// 取得每一筆訂單明細
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
            String sql = @"SELECT OrderID,o.CustomerID,o.EmployeeID,c.CompanyName,LastName+' '+FirstName AS EmpName,convert(char(10),OrderDate,111) AS OrderDate,convert(char(10),OrderDate,111) AS RequiredDate,convert(char(10),ShippedDate,111) AS ShippedDate,o.ShipperID,ship.CompanyName AS ShipperName,Freight,ShipName,ShipAddress,ShipCity,ShipCountry,ShipPostalCode,ShipRegion
                           FROM [Sales].[Orders] AS o                           
	                       JOIN [Sales].[Customers] AS c
	                       ON o.CustomerID=c.CustomerID
						   JOIN [HR].[Employees] AS e
						   ON o.EmployeeID=e.EmployeeID
						   JOIN [Production].[Suppliers] AS ship
						   ON o.[ShipperID]=ship.SupplierID
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
                result.OrderId = Convert.ToInt32(dr["OrderID"]);
                result.CustId = Convert.ToString(dr["CustomerID"]);
                result.EmpId = Convert.ToInt32(dr["EmployeeID"]);
                result.CompanyName = Convert.ToString(dr["CompanyName"]);
                result.EmpName= Convert.ToString(dr["EmpName"]);
                result.ShipperId = Convert.ToInt32(dr["ShipperID"]); 
                result.ShipperName = Convert.ToString(dr["ShipperName"]);
                result.OrderDate = Convert.ToString(dr["OrderDate"]);
                result.RequireDate = Convert.ToString(dr["RequiredDate"]);
                result.ShippedDate = Convert.ToString(dr["ShippedDate"]);
                result.Freight = Convert.ToInt32(dr["Freight"]); 
                result.ShipName=Convert.ToString(dr["ShipName"]);             
                result.ShipAddress= Convert.ToString(dr["ShipAddress"]);
                result.ShipCity= Convert.ToString(dr["ShipCity"]);
                result.ShipRegion= Convert.ToString(dr["ShipRegion"]);
                result.ShipPostalCode= Convert.ToString(dr["ShipPostalCode"]);
                result.ShipCountry= Convert.ToString(dr["ShipCountry"]);             
                   
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