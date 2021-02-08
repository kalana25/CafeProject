using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class Order
    {
        public DataTable RetrieveFoodBevarage()
        {
            string query = "select Id,Code,Name,Price,Description from Product";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int SaveOrderHeader(DateTime date,decimal amount)
        {
            string orderheader = $"insert into OrderHeader(OrderDate,Amount,OrderStatus) values(CAST('{date.ToString("yyyy-MM-dd")}' AS DATETIME),{amount},'ToDo')";
            string selectquery = $"SELECT IDENT_CURRENT('OrderHeader')";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(orderheader, con);
                SqlCommand selectCommand = new SqlCommand(selectquery, con);
                try
                {
                    int orderHeaderId = 0;
                    con.Open();
                    if(command.ExecuteNonQuery()>0)
                    {
                        orderHeaderId = Convert.ToInt32(selectCommand.ExecuteScalar());
                    }
                    return orderHeaderId;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int SaveOrderMenuDetails(int orderId,int setMenuId,int quantity,decimal total)
        {
            string query = $"insert into OrderMenu(OrderId,SetMenuId,Quantity,Total) values({orderId},{setMenuId},{quantity},{total})";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int SaveOrderProductDetails(int orderId, int productId, int quantity, decimal total)
        {
            string query = $"insert into OrderProduct(OrderId,ProductId,Quantity,Total) values({orderId},{productId},{quantity},{total})";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DataTable RetrieveOrderHeader(string status)
        {
            string query = $"select Id,OrderDate,Amount,OrderStatus from OrderHeader where OrderStatus='{status}'";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public DataTable RetrieveOrderDetails(int orderId)
        {
            string query = $"select [Name],[Quantity],[Total] from[OrderProduct] op inner join[Product] p on op.ProductId = p.Id where op.OrderId = {orderId} union select[Name],[Quantity],[Total] from[OrderMenu] om inner join[SetMenu] m on om.SetMenuId = m.Id where om.OrderId = {orderId}";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);
                        return dt;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int UpdateOrder (int orderId)
        {
            string query = $"update OrderHeader set OrderStatus='Complete' where Id={orderId}";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    return command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public string GetNextOrderRef()
        {


            return "";
        }
    }
}
