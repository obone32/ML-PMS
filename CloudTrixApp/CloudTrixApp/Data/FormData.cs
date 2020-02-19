using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class FormData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[FormSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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
        public static List<Form> List()
        {
            List<Form> FormList = new List<Form>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[FormSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Form Form = new Form();
                while (reader.Read())
                {
                    Form = new Form();
                    Form.FormID = System.Convert.ToInt32(reader["FormID"]);
                    Form.FormName = Convert.ToString(reader["FormName"]);
                    FormList.Add(Form);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return FormList;
            }
            finally
            {
                connection.Close();
            }
            return FormList;
        }
    }
}