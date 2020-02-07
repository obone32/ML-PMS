using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class TaskAssignment_TaskData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAssignment_TaskSelect]";
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

        public static List<Task> List()
        {
            List<Task> TaskList = new List<Task>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[TaskAssignment_TaskSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                Task Task = new Task();
                while (reader.Read())
                {
                    Task = new Task();
                    Task.TaskID = System.Convert.ToInt32(reader["TaskID"]);
                    Task.TaskName = Convert.ToString(reader["TaskName"]);
                    TaskList.Add(Task);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return TaskList;
            }
            finally
            {
                connection.Close();
            }
            return TaskList;
        }

    }

    public class TaskAssignment_EmployeeData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAssignment_EmployeeSelect]";
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
            String selectProcedure = "[TaskAssignment_EmployeeSelect]";
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

    public class TaskAssignment_TaskStateData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAssignment_TaskStateSelect]";
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

        public static List<TaskState> List()
        {
            List<TaskState> TaskStateList = new List<TaskState>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[TaskAssignment_TaskStateSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                TaskState TaskState = new TaskState();
                while (reader.Read())
                {
                    TaskState = new TaskState();
                    TaskState.TaskStateID = System.Convert.ToInt32(reader["TaskStateID"]);
                    TaskState.TaskStateName = Convert.ToString(reader["TaskStateName"]);
                    TaskStateList.Add(TaskState);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return TaskStateList;
            }
            finally
            {
                connection.Close();
            }
            return TaskStateList;
        }

    }

}

 
