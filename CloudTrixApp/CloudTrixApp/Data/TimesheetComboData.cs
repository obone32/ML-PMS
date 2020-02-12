using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class Timesheet_EmployeeData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Timesheet_EmployeeSelect]";
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

        public static List<Employee> List()
        {
            List<Employee> EmployeeList = new List<Employee>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[Timesheet_EmployeeSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Employee Employee = new Employee();
                while (reader.Read())
                {
                    Employee = new Employee();
                    Employee.EmployeeID = System.Convert.ToInt32(reader["EmployeeID"]);
                    Employee.FirstName = Convert.ToString(reader["FirstName"]);
                    EmployeeList.Add(Employee);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return EmployeeList;
            }
            finally
            {
                connection.Close();
            }
            return EmployeeList;
        }

    }

    public class Timesheet_ProjectData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Timesheet_ProjectSelect]";
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
            String selectProcedure = "[Timesheet_ProjectSelect]";
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

 
