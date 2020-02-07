using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class Project_ProjectStatusData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Project_ProjectStatusSelect]";
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

        public static List<ProjectStatus> List()
        {
            List<ProjectStatus> ProjectStatusList = new List<ProjectStatus>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[Project_ProjectStatusSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                ProjectStatus ProjectStatus = new ProjectStatus();
                while (reader.Read())
                {
                    ProjectStatus = new ProjectStatus();
                    ProjectStatus.ProjectStatusID = System.Convert.ToInt32(reader["ProjectStatusID"]);
                    ProjectStatus.ProjectStatusName = Convert.ToString(reader["ProjectStatusName"]);
                    ProjectStatusList.Add(ProjectStatus);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ProjectStatusList;
            }
            finally
            {
                connection.Close();
            }
            return ProjectStatusList;
        }

    }

    public class Project_ClientData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Project_ClientSelect]";
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
            String selectProcedure = "[Project_ClientSelect]";
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

    public class Project_ArchitectData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Project_ArchitectSelect]";
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
            String selectProcedure = "[Project_ArchitectSelect]";
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

    public class Project_CompanyData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Project_CompanySelect]";
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
            String selectProcedure = "[Project_CompanySelect]";
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

 
