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

    }
}
