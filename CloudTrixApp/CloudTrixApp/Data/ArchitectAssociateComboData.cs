using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class ArchitectAssociate_ArchitectData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ArchitectAssociate_ArchitectSelect]";
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

        public static List<Architect> List()
        {
            List<Architect> ArchitectList = new List<Architect>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[ArchitectAssociate_ArchitectSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Architect Architect = new Architect();
                while (reader.Read())
                {
                    Architect = new Architect();
                    Architect.ArchitectID = System.Convert.ToInt32(reader["ArchitectID"]);
                    Architect.ArchitectName = Convert.ToString(reader["ArchitectName"]);
                    ArchitectList.Add(Architect);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ArchitectList;
            }
            finally
            {
                connection.Close();
            }
            return ArchitectList;
        }

    }

}

 
