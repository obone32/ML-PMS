using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class TimesheetData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TimesheetSelectAll]";
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
            string selectProcedure = "[TimesheetSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Timesheet I D") {
                selectCommand.Parameters.AddWithValue("@TimesheetID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TimesheetID", DBNull.Value); }
            if (sField == "Employee I D") {
                selectCommand.Parameters.AddWithValue("@FirstName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@FirstName", DBNull.Value); }
            if (sField == "Project I D") {
                selectCommand.Parameters.AddWithValue("@ProjectName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectName", DBNull.Value); }
            if (sField == "Entry Date") {
                selectCommand.Parameters.AddWithValue("@EntryDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@EntryDate", DBNull.Value); }
            if (sField == "Start Time") {
                selectCommand.Parameters.AddWithValue("@StartTime", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@StartTime", DBNull.Value); }
            if (sField == "End Time") {
                selectCommand.Parameters.AddWithValue("@EndTime", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@EndTime", DBNull.Value); }
            if (sField == "Tot Time") {
                selectCommand.Parameters.AddWithValue("@TotTime", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TotTime", DBNull.Value); }
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

        public static Timesheet Select_Record(Timesheet TimesheetPara)
        {
            Timesheet Timesheet = new Timesheet();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[TimesheetSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@TimesheetID", TimesheetPara.TimesheetID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Timesheet.TimesheetID = System.Convert.ToInt32(reader["TimesheetID"]);
                    Timesheet.EmployeeID = System.Convert.ToInt32(reader["EmployeeID"]);
                    Timesheet.ProjectID = System.Convert.ToInt32(reader["ProjectID"]);
                    Timesheet.EntryDate = System.Convert.ToDateTime(reader["EntryDate"]);
                    Timesheet.StartTime = System.Convert.ToDateTime(reader["StartTime"]);
                    Timesheet.EndTime = System.Convert.ToDateTime(reader["EndTime"]);
                    Timesheet.TotTime = reader["TotTime"] is DBNull ? null : reader["TotTime"].ToString();
                    Timesheet.Remarks = System.Convert.ToString(reader["Remarks"]);
                }
                else
                {
                    Timesheet = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Timesheet;
            }
            finally
            {
                connection.Close();
            }
            return Timesheet;
        }

        public static bool Add(Timesheet Timesheet)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[TimesheetInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@EmployeeID", Timesheet.EmployeeID);
            insertCommand.Parameters.AddWithValue("@ProjectID", Timesheet.ProjectID);
            insertCommand.Parameters.AddWithValue("@EntryDate", Timesheet.EntryDate);
            insertCommand.Parameters.AddWithValue("@StartTime", Timesheet.StartTime);
            insertCommand.Parameters.AddWithValue("@EndTime", Timesheet.EndTime);
            if (Timesheet.TotTime != null) {
                insertCommand.Parameters.AddWithValue("@TotTime", Timesheet.TotTime);
            } else {
                insertCommand.Parameters.AddWithValue("@TotTime", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@Remarks", Timesheet.Remarks);
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

        public static bool Update(Timesheet oldTimesheet, 
               Timesheet newTimesheet)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[TimesheetUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewEmployeeID", newTimesheet.EmployeeID);
            updateCommand.Parameters.AddWithValue("@NewProjectID", newTimesheet.ProjectID);
            updateCommand.Parameters.AddWithValue("@NewEntryDate", newTimesheet.EntryDate);
            updateCommand.Parameters.AddWithValue("@NewStartTime", newTimesheet.StartTime);
            updateCommand.Parameters.AddWithValue("@NewEndTime", newTimesheet.EndTime);
            if (newTimesheet.TotTime != null) {
                updateCommand.Parameters.AddWithValue("@NewTotTime", newTimesheet.TotTime);
            } else {
                updateCommand.Parameters.AddWithValue("@NewTotTime", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewRemarks", newTimesheet.Remarks);
            updateCommand.Parameters.AddWithValue("@OldTimesheetID", oldTimesheet.TimesheetID);
            updateCommand.Parameters.AddWithValue("@OldEmployeeID", oldTimesheet.EmployeeID);
            updateCommand.Parameters.AddWithValue("@OldProjectID", oldTimesheet.ProjectID);
            updateCommand.Parameters.AddWithValue("@OldEntryDate", oldTimesheet.EntryDate);
            updateCommand.Parameters.AddWithValue("@OldStartTime", oldTimesheet.StartTime);
            updateCommand.Parameters.AddWithValue("@OldEndTime", oldTimesheet.EndTime);
            if (oldTimesheet.TotTime != null) {
                updateCommand.Parameters.AddWithValue("@OldTotTime", oldTimesheet.TotTime);
            } else {
                updateCommand.Parameters.AddWithValue("@OldTotTime", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldRemarks", oldTimesheet.Remarks);
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

        public static bool Delete(Timesheet Timesheet)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[TimesheetDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldTimesheetID", Timesheet.TimesheetID);
            deleteCommand.Parameters.AddWithValue("@OldEmployeeID", Timesheet.EmployeeID);
            deleteCommand.Parameters.AddWithValue("@OldProjectID", Timesheet.ProjectID);
            deleteCommand.Parameters.AddWithValue("@OldEntryDate", Timesheet.EntryDate);
            deleteCommand.Parameters.AddWithValue("@OldStartTime", Timesheet.StartTime);
            deleteCommand.Parameters.AddWithValue("@OldEndTime", Timesheet.EndTime);
            if (Timesheet.TotTime != null) {
                deleteCommand.Parameters.AddWithValue("@OldTotTime", Timesheet.TotTime);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldTotTime", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldRemarks", Timesheet.Remarks);
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
 
