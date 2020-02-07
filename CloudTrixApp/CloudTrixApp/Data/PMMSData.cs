using System;
using System.Data;
using System.Data.SqlClient;

namespace CloudTrixApp.Data
{
    public class PMMSData
    {
        public static string connectionString
                = "Data Source=103.21.58.192;Initial Catalog=LPMMS;User Id=pmsjayesh;Password=LatestPm@123;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}



 
