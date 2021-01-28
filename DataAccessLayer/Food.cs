using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class Food
    {
        public int insert(string code, string name, decimal price, string description)
        {
            string query = $"insert into Product(Code,Name,Price,Description,Type) values('{code}','{name}',{price},'{description}','Food')";
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
            string query = $"delete Product where Id={id}";
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

        public int Edit(int id, string code, string name, decimal price, string description)
        {
            string query = $"update Product set Code='{code}',Name='{name}',Price={price},Description='{description}' where Id={id}";
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

        public DataTable Retrieve()
        {
            string query = "select Id,Code,Name,Price,Description from Product where Type='Food'";
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
    }
}
