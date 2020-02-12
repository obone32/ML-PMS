using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class BackupLogData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[BackupLogSelectAll]";
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
            string selectProcedure = "[BackupLogSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Backup Log I D") {
                selectCommand.Parameters.AddWithValue("@BackupLogID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@BackupLogID", DBNull.Value); }
            if (sField == "Backup Date") {
                selectCommand.Parameters.AddWithValue("@BackupDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@BackupDate", DBNull.Value); }
            if (sField == "File Path") {
                selectCommand.Parameters.AddWithValue("@FilePath", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@FilePath", DBNull.Value); }
            if (sField == "Remarks") {
                selectCommand.Parameters.AddWithValue("@Remarks", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Remarks", DBNull.Value); }
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

        public static BackupLog Select_Record(BackupLog BackupLogPara)
        {
            BackupLog BackupLog = new BackupLog();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[BackupLogSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@BackupLogID", BackupLogPara.BackupLogID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    BackupLog.BackupLogID = System.Convert.ToInt32(reader["BackupLogID"]);
                    BackupLog.BackupDate = System.Convert.ToDateTime(reader["BackupDate"]);
                    BackupLog.FilePath = System.Convert.ToString(reader["FilePath"]);
                    BackupLog.Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString();
                }
                else
                {
                    BackupLog = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return BackupLog;
            }
            finally
            {
                connection.Close();
            }
            return BackupLog;
        }

        public static bool Add(BackupLog BackupLog)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[BackupLogInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@BackupDate", BackupLog.BackupDate);
            insertCommand.Parameters.AddWithValue("@FilePath", BackupLog.FilePath);
            if (BackupLog.Remarks != null) {
                insertCommand.Parameters.AddWithValue("@Remarks", BackupLog.Remarks);
            } else {
                insertCommand.Parameters.AddWithValue("@Remarks", DBNull.Value); }
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

        public static bool Update(BackupLog oldBackupLog, 
               BackupLog newBackupLog)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[BackupLogUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewBackupDate", newBackupLog.BackupDate);
            updateCommand.Parameters.AddWithValue("@NewFilePath", newBackupLog.FilePath);
            if (newBackupLog.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@NewRemarks", newBackupLog.Remarks);
            } else {
                updateCommand.Parameters.AddWithValue("@NewRemarks", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldBackupLogID", oldBackupLog.BackupLogID);
            updateCommand.Parameters.AddWithValue("@OldBackupDate", oldBackupLog.BackupDate);
            updateCommand.Parameters.AddWithValue("@OldFilePath", oldBackupLog.FilePath);
            if (oldBackupLog.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@OldRemarks", oldBackupLog.Remarks);
            } else {
                updateCommand.Parameters.AddWithValue("@OldRemarks", DBNull.Value); }
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

        public static bool Delete(BackupLog BackupLog)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[BackupLogDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldBackupLogID", BackupLog.BackupLogID);
            deleteCommand.Parameters.AddWithValue("@OldBackupDate", BackupLog.BackupDate);
            deleteCommand.Parameters.AddWithValue("@OldFilePath", BackupLog.FilePath);
            if (BackupLog.Remarks != null) {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", BackupLog.Remarks);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", DBNull.Value); }
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
 
