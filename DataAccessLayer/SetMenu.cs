using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class SetMenu
    {
        //Menu header
        public DataTable Retrieve()
        {
            string query = "select Id,Code,Name,Price from SetMenu";
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

        public int insert(string code, string name, decimal price)
        {
            string query = $"insert into SetMenu(Code,Name,Price) values('{code}','{name}',{price})";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int Delete(int id)
        {
            string query = $"delete SetMenu where Id={id}";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int Edit(int id, string code, string name, decimal price)
        {
            string query = $"update SetMenu set Code='{code}',Name='{name}',Price={price} where Id={id}";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                try
                {
                    con.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        //Menu Items

        public DataTable RetrieveMenuItems(int menuId)
        {
            string query = $"select S.Id,P.Code,P.Name,P.Price,S.Quantity from SetMenuItem S INNER JOIN Product P ON S.ProductId=P.Id WHERE SetMenuId='{menuId}'";
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

        public int insertMenuItem(int productId, int quantity,int menuId,decimal price)
        {
            string query = $"insert into SetMenuItem(ProductId,Quantity,SetMenuId) values({productId},{quantity},{menuId})";
            string priceUpdateQuery = $"update SetMenu set Price=Price+{price*quantity} where Id={menuId}";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                SqlCommand updateCmd = new SqlCommand(priceUpdateQuery, con);
                try
                {
                    con.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    rowsAffected = updateCmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int DeleteMenuItem(int menuItemId,int quantity,decimal price,int menuId)
        {
            string query = $"delete SetMenuItem where Id={menuItemId}";
            string priceUpdateQuery = $"update SetMenu set Price=Price-{price * quantity} where Id={menuId}";
            using (SqlConnection con = new SqlConnection(Database.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query, con);
                SqlCommand updateCmd = new SqlCommand(priceUpdateQuery, con);
                try
                {
                    con.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    rowsAffected = updateCmd.ExecuteNonQuery();
                    return rowsAffected;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
