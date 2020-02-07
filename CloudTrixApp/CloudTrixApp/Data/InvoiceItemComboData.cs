using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class InvoiceItem_InvoiceData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceItem_InvoiceSelect]";
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

        public static List<Invoice> List()
        {
            List<Invoice> InvoiceList = new List<Invoice>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[InvoiceItem_InvoiceSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Invoice Invoice = new Invoice();
                while (reader.Read())
                {
                    Invoice = new Invoice();
                    Invoice.InvoiceID = System.Convert.ToInt32(reader["InvoiceID"]);
                    InvoiceList.Add(Invoice);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return InvoiceList;
            }
            finally
            {
                connection.Close();
            }
            return InvoiceList;
        }

    }

}

 
