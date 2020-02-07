using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class ProjectAssignmentData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ProjectAssignmentSelectAll]";
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
            string selectProcedure = "[ProjectAssignmentSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Project Assignment I D") {
                selectCommand.Parameters.AddWithValue("@ProjectAssignmentID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectAssignmentID", DBNull.Value); }
            if (sField == "Project I D") {
                selectCommand.Parameters.AddWithValue("@ProjectName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectName", DBNull.Value); }
            if (sField == "Employee I D") {
                selectCommand.Parameters.AddWithValue("@FirstName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@FirstName", DBNull.Value); }
            if (sField == "Assignment Date") {
                selectCommand.Parameters.AddWithValue("@AssignmentDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AssignmentDate", DBNull.Value); }
            if (sField == "Remarks") {
                selectCommand.Parameters.AddWithValue("@Remarks", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Remarks", DBNull.Value); }
            if (sField == "Add User I D") {
                selectCommand.Parameters.AddWithValue("@AddUserID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AddUserID", DBNull.Value); }
            if (sField == "Add Date") {
                selectCommand.Parameters.AddWithValue("@AddDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AddDate", DBNull.Value); }
            if (sField == "Archive User I D") {
                selectCommand.Parameters.AddWithValue("@ArchiveUserID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value); }
            if (sField == "Archive Date") {
                selectCommand.Parameters.AddWithValue("@ArchiveDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ArchiveDate", DBNull.Value); }
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

        public static ProjectAssignment Select_Record(ProjectAssignment ProjectAssignmentPara)
        {
            ProjectAssignment ProjectAssignment = new ProjectAssignment();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ProjectAssignmentSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ProjectAssignmentID", ProjectAssignmentPara.ProjectAssignmentID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ProjectAssignment.ProjectAssignmentID = System.Convert.ToInt32(reader["ProjectAssignmentID"]);
                    ProjectAssignment.ProjectID = System.Convert.ToInt32(reader["ProjectID"]);
                    ProjectAssignment.EmployeeID = System.Convert.ToInt32(reader["EmployeeID"]);
                    ProjectAssignment.AssignmentDate = System.Convert.ToDateTime(reader["AssignmentDate"]);
                    ProjectAssignment.Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString();
                    ProjectAssignment.AddUserID = System.Convert.ToInt32(reader["AddUserID"]);
                    ProjectAssignment.AddDate = System.Convert.ToDateTime(reader["AddDate"]);
                    ProjectAssignment.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    ProjectAssignment.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                }
                else
                {
                    ProjectAssignment = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ProjectAssignment;
            }
            finally
            {
                connection.Close();
            }
            return ProjectAssignment;
        }

        public static bool Add(ProjectAssignment ProjectAssignment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[ProjectAssignmentInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@ProjectID", ProjectAssignment.ProjectID);
            insertCommand.Parameters.AddWithValue("@EmployeeID", ProjectAssignment.EmployeeID);
            insertCommand.Parameters.AddWithValue("@AssignmentDate", ProjectAssignment.AssignmentDate);
            if (ProjectAssignment.Remarks != null) {
                insertCommand.Parameters.AddWithValue("@Remarks", ProjectAssignment.Remarks);
            } else {
                insertCommand.Parameters.AddWithValue("@Remarks", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@AddUserID", ProjectAssignment.AddUserID);
            insertCommand.Parameters.AddWithValue("@AddDate", ProjectAssignment.AddDate);
            if (ProjectAssignment.ArchiveUserID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", ProjectAssignment.ArchiveUserID);
            } else {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value); }
            if (ProjectAssignment.ArchiveDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", ProjectAssignment.ArchiveDate);
            } else {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", DBNull.Value); }
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

        public static bool Update(ProjectAssignment oldProjectAssignment, 
               ProjectAssignment newProjectAssignment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[ProjectAssignmentUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewProjectID", newProjectAssignment.ProjectID);
            updateCommand.Parameters.AddWithValue("@NewEmployeeID", newProjectAssignment.EmployeeID);
            updateCommand.Parameters.AddWithValue("@NewAssignmentDate", newProjectAssignment.AssignmentDate);
            if (newProjectAssignment.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@NewRemarks", newProjectAssignment.Remarks);
            } else {
                updateCommand.Parameters.AddWithValue("@NewRemarks", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewAddUserID", newProjectAssignment.AddUserID);
            updateCommand.Parameters.AddWithValue("@NewAddDate", newProjectAssignment.AddDate);
            if (newProjectAssignment.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", newProjectAssignment.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", DBNull.Value); }
            if (newProjectAssignment.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", newProjectAssignment.ArchiveDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldProjectAssignmentID", oldProjectAssignment.ProjectAssignmentID);
            updateCommand.Parameters.AddWithValue("@OldProjectID", oldProjectAssignment.ProjectID);
            updateCommand.Parameters.AddWithValue("@OldEmployeeID", oldProjectAssignment.EmployeeID);
            updateCommand.Parameters.AddWithValue("@OldAssignmentDate", oldProjectAssignment.AssignmentDate);
            if (oldProjectAssignment.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@OldRemarks", oldProjectAssignment.Remarks);
            } else {
                updateCommand.Parameters.AddWithValue("@OldRemarks", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldAddUserID", oldProjectAssignment.AddUserID);
            updateCommand.Parameters.AddWithValue("@OldAddDate", oldProjectAssignment.AddDate);
            if (oldProjectAssignment.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", oldProjectAssignment.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (oldProjectAssignment.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", oldProjectAssignment.ArchiveDate);
            } else {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", DBNull.Value); }
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

        public static bool Delete(ProjectAssignment ProjectAssignment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[ProjectAssignmentDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldProjectAssignmentID", ProjectAssignment.ProjectAssignmentID);
            deleteCommand.Parameters.AddWithValue("@OldProjectID", ProjectAssignment.ProjectID);
            deleteCommand.Parameters.AddWithValue("@OldEmployeeID", ProjectAssignment.EmployeeID);
            deleteCommand.Parameters.AddWithValue("@OldAssignmentDate", ProjectAssignment.AssignmentDate);
            if (ProjectAssignment.Remarks != null) {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", ProjectAssignment.Remarks);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldAddUserID", ProjectAssignment.AddUserID);
            deleteCommand.Parameters.AddWithValue("@OldAddDate", ProjectAssignment.AddDate);
            if (ProjectAssignment.ArchiveUserID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", ProjectAssignment.ArchiveUserID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (ProjectAssignment.ArchiveDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", ProjectAssignment.ArchiveDate);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", DBNull.Value); }
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
 
