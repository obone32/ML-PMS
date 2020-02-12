using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class TaskAttachmentData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAttachmentSelectAll]";
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
            string selectProcedure = "[TaskAttachmentSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Task I D") {
                selectCommand.Parameters.AddWithValue("@TaskID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskID", DBNull.Value); }
            if (sField == "Task Attachment I D") {
                selectCommand.Parameters.AddWithValue("@TaskAttachmentID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TaskAttachmentID", DBNull.Value); }
            if (sField == "Attachment Name") {
                selectCommand.Parameters.AddWithValue("@AttachmentName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AttachmentName", DBNull.Value); }
            if (sField == "Decription") {
                selectCommand.Parameters.AddWithValue("@Decription", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Decription", DBNull.Value); }
            if (sField == "File Path") {
                selectCommand.Parameters.AddWithValue("@FilePath", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@FilePath", DBNull.Value); }
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

        public static TaskAttachment Select_Record(TaskAttachment TaskAttachmentPara)
        {
            TaskAttachment TaskAttachment = new TaskAttachment();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TaskAttachmentSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TaskID", TaskAttachmentPara.TaskID);
            selectCommand.Parameters.AddWithValue("@TaskAttachmentID", TaskAttachmentPara.TaskAttachmentID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    TaskAttachment.TaskID = System.Convert.ToInt32(reader["TaskID"]);
                    TaskAttachment.TaskAttachmentID = System.Convert.ToInt32(reader["TaskAttachmentID"]);
                    TaskAttachment.AttachmentName = System.Convert.ToString(reader["AttachmentName"]);
                    TaskAttachment.Decription = reader["Decription"] is DBNull ? null : reader["Decription"].ToString();
                    TaskAttachment.FilePath = System.Convert.ToString(reader["FilePath"]);
                }
                else
                {
                    TaskAttachment = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return TaskAttachment;
            }
            finally
            {
                connection.Close();
            }
            return TaskAttachment;
        }

        public static bool Add(TaskAttachment TaskAttachment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[TaskAttachmentInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@TaskID", TaskAttachment.TaskID);
            insertCommand.Parameters.AddWithValue("@TaskAttachmentID", TaskAttachment.TaskAttachmentID);
            insertCommand.Parameters.AddWithValue("@AttachmentName", TaskAttachment.AttachmentName);
            if (TaskAttachment.Decription != null) {
                insertCommand.Parameters.AddWithValue("@Decription", TaskAttachment.Decription);
            } else {
                insertCommand.Parameters.AddWithValue("@Decription", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@FilePath", TaskAttachment.FilePath);
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

        public static bool Update(TaskAttachment oldTaskAttachment, 
               TaskAttachment newTaskAttachment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[TaskAttachmentUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewTaskID", newTaskAttachment.TaskID);
            updateCommand.Parameters.AddWithValue("@NewTaskAttachmentID", newTaskAttachment.TaskAttachmentID);
            updateCommand.Parameters.AddWithValue("@NewAttachmentName", newTaskAttachment.AttachmentName);
            if (newTaskAttachment.Decription != null) {
                updateCommand.Parameters.AddWithValue("@NewDecription", newTaskAttachment.Decription);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDecription", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewFilePath", newTaskAttachment.FilePath);
            updateCommand.Parameters.AddWithValue("@OldTaskID", oldTaskAttachment.TaskID);
            updateCommand.Parameters.AddWithValue("@OldTaskAttachmentID", oldTaskAttachment.TaskAttachmentID);
            updateCommand.Parameters.AddWithValue("@OldAttachmentName", oldTaskAttachment.AttachmentName);
            if (oldTaskAttachment.Decription != null) {
                updateCommand.Parameters.AddWithValue("@OldDecription", oldTaskAttachment.Decription);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDecription", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldFilePath", oldTaskAttachment.FilePath);
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

        public static bool Delete(TaskAttachment TaskAttachment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[TaskAttachmentDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldTaskID", TaskAttachment.TaskID);
            deleteCommand.Parameters.AddWithValue("@OldTaskAttachmentID", TaskAttachment.TaskAttachmentID);
            deleteCommand.Parameters.AddWithValue("@OldAttachmentName", TaskAttachment.AttachmentName);
            if (TaskAttachment.Decription != null) {
                deleteCommand.Parameters.AddWithValue("@OldDecription", TaskAttachment.Decription);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDecription", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldFilePath", TaskAttachment.FilePath);
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
 
