﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class Discount
    {
        public DataTable Retrieve()
        {
            string query = "select D.Id,D.SetMenuId,D.Rate,D.StartDate,D.EndDate,M.Name from MenuDiscount D Inner join SetMenu M On D.SetMenuId=M.Id";
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

        public int insert(int menuId, decimal rate, DateTime startDate,DateTime endDate)
        {
            string query = $"insert into MenuDiscount(SetMenuId,Rate,StartDate,EndDate) values({menuId},{rate},CAST('{startDate.ToString("yyyy-MM-dd")}' AS DATETIME),CAST('{endDate.ToString("yyyy-MM-dd")}' AS DATETIME))";
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
            string query = $"delete MenuDiscount where Id={id}";
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

        public int Edit(int id,int menuId, decimal rate, DateTime startDate, DateTime endDate)
        {
            string query = $"update MenuDiscount set SetMenuId={menuId}, Rate={rate}, StartDate=CAST('{startDate.ToString("yyyy-MM-dd")}' AS DATETIME), EndDate=CAST('{endDate.ToString("yyyy-MM-dd")}' AS DATETIME) where Id={id}";
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

        public DataTable Retrieve(int setMenuId,DateTime date)
        {
            string query = $"select Id,SetMenuId,Rate,StartDate,EndDate from MenuDiscount where SetMenuId={setMenuId} and StartDate <='{date.ToString("yyyy-MM-dd")}' and EndDate >='{date.ToString("yyyy-MM-dd")}'";
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
