using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public static class Database
    {
		private static string connectionString;

		public static string ConnectionString
		{
			get {
				//"Development": "Server=DESKTOP-R9AHJOC;Database=PointOfSalesKW;Trusted_Connection=True;ConnectRetryCount=0;"
				connectionString = "Data Source=DESKTOP-R9AHJOC;Initial Catalog=CafeDB;Integrated Security=true";
				return connectionString;
			}
		}

	}
}
