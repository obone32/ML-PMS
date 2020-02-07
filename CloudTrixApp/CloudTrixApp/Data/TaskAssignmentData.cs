using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class TaskAssignmentData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAssignmentSelectAll]";
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
            string selectProcedure = "[TaskAssignmentSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Task Assignment I D") {
                selectCommand.Parameters.AddWithValue("@TaskAssignmentID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskAssignmentID", DBNull.Value); }
            if (sField == "Assignment Date") {
                selectCommand.Parameters.AddWithValue("@AssignmentDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AssignmentDate", DBNull.Value); }
            if (sField == "Task I D") {
                selectCommand.Parameters.AddWithValue("@TaskName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskName", DBNull.Value); }
            if (sField == "Employee I D") {
                selectCommand.Parameters.AddWithValue("@FirstName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@FirstName", DBNull.Value); }
            if (sField == "Task State I D") {
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

        public static TaskAssignment Select_Record(TaskAssignment TaskAssignmentPara)
        {
            TaskAssignment TaskAssignment = new TaskAssignment();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAssignmentSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TaskAssignmentID", TaskAssignmentPara.TaskAssignmentID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    TaskAssignment.TaskAssignmentID = System.Convert.ToInt32(reader["TaskAssignmentID"]);
                    TaskAssignment.AssignmentDate = System.Convert.ToDateTime(reader["AssignmentDate"]);
                    TaskAssignment.TaskID = System.Convert.ToInt32(reader["TaskID"]);
                    TaskAssignment.EmployeeID = System.Convert.ToInt32(reader["EmployeeID"]);
                    TaskAssignment.TaskStateID = System.Convert.ToInt32(reader["TaskStateID"]);
                }
                else
                {
                    TaskAssignment = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return TaskAssignment;
            }
            finally
            {
                connection.Close();
            }
            return TaskAssignment;
        }

        public static bool Add(TaskAssignment TaskAssignment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[TaskAssignmentInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@AssignmentDate", TaskAssignment.AssignmentDate);
            insertCommand.Parameters.AddWithValue("@TaskID", TaskAssignment.TaskID);
            insertCommand.Parameters.AddWithValue("@EmployeeID", TaskAssignment.EmployeeID);
            insertCommand.Parameters.AddWithValue("@TaskStateID", TaskAssignment.TaskStateID);
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

        public static bool Update(TaskAssignment oldTaskAssignment, 
               TaskAssignment newTaskAssignment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[TaskAssignmentUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewAssignmentDate", newTaskAssignment.AssignmentDate);
            updateCommand.Parameters.AddWithValue("@NewTaskID", newTaskAssignment.TaskID);
            updateCommand.Parameters.AddWithValue("@NewEmployeeID", newTaskAssignment.EmployeeID);
            updateCommand.Parameters.AddWithValue("@NewTaskStateID", newTaskAssignment.TaskStateID);
            updateCommand.Parameters.AddWithValue("@OldTaskAssignmentID", oldTaskAssignment.TaskAssignmentID);
            updateCommand.Parameters.AddWithValue("@OldAssignmentDate", oldTaskAssignment.AssignmentDate);
            updateCommand.Parameters.AddWithValue("@OldTaskID", oldTaskAssignment.TaskID);
            updateCommand.Parameters.AddWithValue("@OldEmployeeID", oldTaskAssignment.EmployeeID);
            updateCommand.Parameters.AddWithValue("@OldTaskStateID", oldTaskAssignment.TaskStateID);
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

        public static bool Delete(TaskAssignment TaskAssignment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[TaskAssignmentDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldTaskAssignmentID", TaskAssignment.TaskAssignmentID);
            deleteCommand.Parameters.AddWithValue("@OldAssignmentDate", TaskAssignment.AssignmentDate);
            deleteCommand.Parameters.AddWithValue("@OldTaskID", TaskAssignment.TaskID);
            deleteCommand.Parameters.AddWithValue("@OldEmployeeID", TaskAssignment.EmployeeID);
            deleteCommand.Parameters.AddWithValue("@OldTaskStateID", TaskAssignment.TaskStateID);
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
 
