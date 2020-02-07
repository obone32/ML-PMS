using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class TaskStateData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskStateSelectAll]";
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
            string selectProcedure = "[TaskStateSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Task State I D") {
                selectCommand.Parameters.AddWithValue("@TaskStateID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskStateID", DBNull.Value); }
            if (sField == "Task State Name") {
                selectCommand.Parameters.AddWithValue("@TaskStateName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskStateName", DBNull.Value); }
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

        public static TaskState Select_Record(TaskState TaskStatePara)
        {
            TaskState TaskState = new TaskState();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskStateSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TaskStateID", TaskStatePara.TaskStateID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    TaskState.TaskStateID = System.Convert.ToInt32(reader["TaskStateID"]);
                    TaskState.TaskStateName = System.Convert.ToString(reader["TaskStateName"]);
                }
                else
                {
                    TaskState = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return TaskState;
            }
            finally
            {
                connection.Close();
            }
            return TaskState;
        }

        public static bool Add(TaskState TaskState)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[TaskStateInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@TaskStateName", TaskState.TaskStateName);
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

        public static bool Update(TaskState oldTaskState, 
               TaskState newTaskState)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[TaskStateUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewTaskStateName", newTaskState.TaskStateName);
            updateCommand.Parameters.AddWithValue("@OldTaskStateID", oldTaskState.TaskStateID);
            updateCommand.Parameters.AddWithValue("@OldTaskStateName", oldTaskState.TaskStateName);
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

        public static bool Delete(TaskState TaskState)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[TaskStateDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldTaskStateID", TaskState.TaskStateID);
            deleteCommand.Parameters.AddWithValue("@OldTaskStateName", TaskState.TaskStateName);
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
 
