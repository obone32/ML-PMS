using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class AdvancePayment_CompanyData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[AdvancePayment_CompanySelect]";
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
            String selectProcedure = "[AdvancePayment_CompanySelect]";
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

    public class AdvancePayment_ClientData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[AdvancePayment_ClientSelect]";
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

        public static List<Client> List()
        {
            List<Client> ClientList = new List<Client>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[AdvancePayment_ClientSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Client Client = new Client();
                while (reader.Read())
                {
                    Client = new Client();
                    Client.ClientID = System.Convert.ToInt32(reader["ClientID"]);
                    Client.ClientName = Convert.ToString(reader["ClientName"]);
                    ClientList.Add(Client);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ClientList;
            }
            finally
            {
                connection.Close();
            }
            return ClientList;
        }

    }

    public class AdvancePayment_ProjectData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[AdvancePayment_ProjectSelect]";
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

        public static List<Project> List()
        {
            List<Project> ProjectList = new List<Project>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[AdvancePayment_ProjectSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Project Project = new Project();
                while (reader.Read())
                {
                    Project = new Project();
                    Project.ProjectID = System.Convert.ToInt32(reader["ProjectID"]);
                    Project.ProjectName = Convert.ToString(reader["ProjectName"]);
                    ProjectList.Add(Project);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ProjectList;
            }
            finally
            {
                connection.Close();
            }
            return ProjectList;
        }

    }

}

 
