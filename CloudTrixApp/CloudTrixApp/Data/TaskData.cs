using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class TaskData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskSelectAll]";
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

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Task I D") {
                selectCommand.Parameters.AddWithValue("@TaskID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskID", DBNull.Value); }
            if (sField == "Task Name") {
                selectCommand.Parameters.AddWithValue("@TaskName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskName", DBNull.Value); }
            if (sField == "Description") {
                selectCommand.Parameters.AddWithValue("@Description", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Description", DBNull.Value); }
            if (sField == "Creation Date") {
                selectCommand.Parameters.AddWithValue("@CreationDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CreationDate", DBNull.Value); }
            selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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

        public static Task Select_Record(Task TaskPara)
        {
            Task Task = new Task();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TaskID", TaskPara.TaskID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Task.TaskID = System.Convert.ToInt32(reader["TaskID"]);
                    Task.TaskName = System.Convert.ToString(reader["TaskName"]);
                    Task.Description = reader["Description"] is DBNull ? null : reader["Description"].ToString();
                    Task.CreationDate = System.Convert.ToDateTime(reader["CreationDate"]);
                }
                else
                {
                    Task = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Task;
            }
            finally
            {
                connection.Close();
            }
            return Task;
        }

        public static bool Add(Task Task)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[TaskInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@TaskName", Task.TaskName);
            if (Task.Description != null) {
                insertCommand.Parameters.AddWithValue("@Description", Task.Description);
            } else {
                insertCommand.Parameters.AddWithValue("@Description", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@CreationDate", Task.CreationDate);
            insertCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(Task oldTask, 
               Task newTask)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[TaskUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewTaskName", newTask.TaskName);
            if (newTask.Description != null) {
                updateCommand.Parameters.AddWithValue("@NewDescription", newTask.Description);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDescription", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewCreationDate", newTask.CreationDate);
            updateCommand.Parameters.AddWithValue("@OldTaskID", oldTask.TaskID);
            updateCommand.Parameters.AddWithValue("@OldTaskName", oldTask.TaskName);
            if (oldTask.Description != null) {
                updateCommand.Parameters.AddWithValue("@OldDescription", oldTask.Description);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDescription", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCreationDate", oldTask.CreationDate);
            updateCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            updateCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                updateCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(updateCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Delete(Task Task)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[TaskDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldTaskID", Task.TaskID);
            deleteCommand.Parameters.AddWithValue("@OldTaskName", Task.TaskName);
            if (Task.Description != null) {
                deleteCommand.Parameters.AddWithValue("@OldDescription", Task.Description);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDescription", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldCreationDate", Task.CreationDate);
            deleteCommand.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int);
            deleteCommand.Parameters["@ReturnValue"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
 
