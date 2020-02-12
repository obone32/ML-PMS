using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class Employee_CompanyData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Employee_CompanySelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader); }
                reader.Close();
            }
            catch (SqlException)
            {
                return dt;
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static List<Company> List()
        {
            List<Company> CompanyList = new List<Company>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[Employee_CompanySelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Company Company = new Company();
                while (reader.Read())
                {
                    Company = new Company();
                    Company.CompanyID = System.Convert.ToInt32(reader["CompanyID"]);
                    Company.CompanyName = Convert.ToString(reader["CompanyName"]);
                    CompanyList.Add(Company);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return CompanyList;
            }
            finally
            {
                connection.Close();
            }
            return CompanyList;
        }

    }

}

 
