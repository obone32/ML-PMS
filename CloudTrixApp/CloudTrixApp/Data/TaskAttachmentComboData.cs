using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{

    public class TaskAttachment_TaskData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAttachment_TaskSelect]";
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
            String selectProcedure = "[TaskAttachment_TaskSelect]";
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

}

 
