using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class ProjectData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ProjectSelectAll]";
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
            string selectProcedure = "[ProjectSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Project I D") {
                selectCommand.Parameters.AddWithValue("@ProjectID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectID", DBNull.Value); }
            if (sField == "Project Name") {
                selectCommand.Parameters.AddWithValue("@ProjectName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectName", DBNull.Value); }
            if (sField == "Billing Name") {
                selectCommand.Parameters.AddWithValue("@BillingName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@BillingName", DBNull.Value); }
            if (sField == "Description") {
                selectCommand.Parameters.AddWithValue("@Description", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Description", DBNull.Value); }
            if (sField == "Location") {
                selectCommand.Parameters.AddWithValue("@Location", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Location", DBNull.Value); }
            if (sField == "Start Date") {
                selectCommand.Parameters.AddWithValue("@StartDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@StartDate", DBNull.Value); }
            if (sField == "End Date") {
                selectCommand.Parameters.AddWithValue("@EndDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@EndDate", DBNull.Value); }
            if (sField == "Project Status I D") {
                selectCommand.Parameters.AddWithValue("@ProjectStatusName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectStatusName", DBNull.Value); }
            if (sField == "Client I D") {
                selectCommand.Parameters.AddWithValue("@ClientName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ClientName", DBNull.Value); }
            if (sField == "Architect I D") {
                selectCommand.Parameters.AddWithValue("@ArchitectName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ArchitectName", DBNull.Value); }
            if (sField == "Company I D") {
                selectCommand.Parameters.AddWithValue("@CompanyName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CompanyName", DBNull.Value); }
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

        public static Project Select_Record(Project ProjectPara)
        {
            Project Project = new Project();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ProjectSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ProjectID", ProjectPara.ProjectID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Project.ProjectID = System.Convert.ToInt32(reader["ProjectID"]);
                    Project.ProjectName = System.Convert.ToString(reader["ProjectName"]);
                    Project.BillingName = reader["BillingName"] is DBNull ? null : reader["BillingName"].ToString();
                    Project.Description = reader["Description"] is DBNull ? null : reader["Description"].ToString();
                    Project.Location = reader["Location"] is DBNull ? null : reader["Location"].ToString();
                    Project.StartDate = System.Convert.ToDateTime(reader["StartDate"]);
                    Project.EndDate = reader["EndDate"] is DBNull ? null : (DateTime?)reader["EndDate"];
                    Project.ProjectStatusID = System.Convert.ToInt32(reader["ProjectStatusID"]);
                    Project.ClientID = System.Convert.ToInt32(reader["ClientID"]);
                    Project.ArchitectID = System.Convert.ToInt32(reader["ArchitectID"]);
                    Project.CompanyID = reader["CompanyID"] is DBNull ? null : (Int32?)reader["CompanyID"];
                    Project.AddUserID = System.Convert.ToInt32(reader["AddUserID"]);
                    Project.AddDate = System.Convert.ToDateTime(reader["AddDate"]);
                    Project.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    Project.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                }
                else
                {
                    Project = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Project;
            }
            finally
            {
                connection.Close();
            }
            return Project;
        }

        public static bool Add(Project Project)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[ProjectInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@ProjectName", Project.ProjectName);
            if (Project.BillingName != null) {
                insertCommand.Parameters.AddWithValue("@BillingName", Project.BillingName);
            } else {
                insertCommand.Parameters.AddWithValue("@BillingName", DBNull.Value); }
            if (Project.Description != null) {
                insertCommand.Parameters.AddWithValue("@Description", Project.Description);
            } else {
                insertCommand.Parameters.AddWithValue("@Description", DBNull.Value); }
            if (Project.Location != null) {
                insertCommand.Parameters.AddWithValue("@Location", Project.Location);
            } else {
                insertCommand.Parameters.AddWithValue("@Location", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@StartDate", Project.StartDate);
            if (Project.EndDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@EndDate", Project.EndDate);
            } else {
                insertCommand.Parameters.AddWithValue("@EndDate", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@ProjectStatusID", Project.ProjectStatusID);
            insertCommand.Parameters.AddWithValue("@ClientID", Project.ClientID);
            insertCommand.Parameters.AddWithValue("@ArchitectID", Project.ArchitectID);
            if (Project.CompanyID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@CompanyID", Project.CompanyID);
            } else {
                insertCommand.Parameters.AddWithValue("@CompanyID", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@AddUserID", Project.AddUserID);
            insertCommand.Parameters.AddWithValue("@AddDate", Project.AddDate);
            if (Project.ArchiveUserID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", Project.ArchiveUserID);
            } else {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value); }
            if (Project.ArchiveDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", Project.ArchiveDate);
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

        public static bool Update(Project oldProject, 
               Project newProject)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[ProjectUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewProjectName", newProject.ProjectName);
            if (newProject.BillingName != null) {
                updateCommand.Parameters.AddWithValue("@NewBillingName", newProject.BillingName);
            } else {
                updateCommand.Parameters.AddWithValue("@NewBillingName", DBNull.Value); }
            if (newProject.Description != null) {
                updateCommand.Parameters.AddWithValue("@NewDescription", newProject.Description);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDescription", DBNull.Value); }
            if (newProject.Location != null) {
                updateCommand.Parameters.AddWithValue("@NewLocation", newProject.Location);
            } else {
                updateCommand.Parameters.AddWithValue("@NewLocation", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewStartDate", newProject.StartDate);
            if (newProject.EndDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewEndDate", newProject.EndDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewEndDate", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewProjectStatusID", newProject.ProjectStatusID);
            updateCommand.Parameters.AddWithValue("@NewClientID", newProject.ClientID);
            updateCommand.Parameters.AddWithValue("@NewArchitectID", newProject.ArchitectID);
            if (newProject.CompanyID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewCompanyID", newProject.CompanyID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCompanyID", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewAddUserID", newProject.AddUserID);
            updateCommand.Parameters.AddWithValue("@NewAddDate", newProject.AddDate);
            if (newProject.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", newProject.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", DBNull.Value); }
            if (newProject.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", newProject.ArchiveDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldProjectID", oldProject.ProjectID);
            updateCommand.Parameters.AddWithValue("@OldProjectName", oldProject.ProjectName);
            if (oldProject.BillingName != null) {
                updateCommand.Parameters.AddWithValue("@OldBillingName", oldProject.BillingName);
            } else {
                updateCommand.Parameters.AddWithValue("@OldBillingName", DBNull.Value); }
            if (oldProject.Description != null) {
                updateCommand.Parameters.AddWithValue("@OldDescription", oldProject.Description);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDescription", DBNull.Value); }
            if (oldProject.Location != null) {
                updateCommand.Parameters.AddWithValue("@OldLocation", oldProject.Location);
            } else {
                updateCommand.Parameters.AddWithValue("@OldLocation", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldStartDate", oldProject.StartDate);
            if (oldProject.EndDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldEndDate", oldProject.EndDate);
            } else {
                updateCommand.Parameters.AddWithValue("@OldEndDate", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldProjectStatusID", oldProject.ProjectStatusID);
            updateCommand.Parameters.AddWithValue("@OldClientID", oldProject.ClientID);
            updateCommand.Parameters.AddWithValue("@OldArchitectID", oldProject.ArchitectID);
            if (oldProject.CompanyID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldCompanyID", oldProject.CompanyID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCompanyID", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldAddUserID", oldProject.AddUserID);
            updateCommand.Parameters.AddWithValue("@OldAddDate", oldProject.AddDate);
            if (oldProject.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", oldProject.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (oldProject.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", oldProject.ArchiveDate);
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

        public static bool Delete(Project Project)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[ProjectDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldProjectID", Project.ProjectID);
            deleteCommand.Parameters.AddWithValue("@OldProjectName", Project.ProjectName);
            if (Project.BillingName != null) {
                deleteCommand.Parameters.AddWithValue("@OldBillingName", Project.BillingName);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldBillingName", DBNull.Value); }
            if (Project.Description != null) {
                deleteCommand.Parameters.AddWithValue("@OldDescription", Project.Description);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDescription", DBNull.Value); }
            if (Project.Location != null) {
                deleteCommand.Parameters.AddWithValue("@OldLocation", Project.Location);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldLocation", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldStartDate", Project.StartDate);
            if (Project.EndDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldEndDate", Project.EndDate);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldEndDate", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldProjectStatusID", Project.ProjectStatusID);
            deleteCommand.Parameters.AddWithValue("@OldClientID", Project.ClientID);
            deleteCommand.Parameters.AddWithValue("@OldArchitectID", Project.ArchitectID);
            if (Project.CompanyID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldCompanyID", Project.CompanyID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCompanyID", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldAddUserID", Project.AddUserID);
            deleteCommand.Parameters.AddWithValue("@OldAddDate", Project.AddDate);
            if (Project.ArchiveUserID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", Project.ArchiveUserID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (Project.ArchiveDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", Project.ArchiveDate);
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
 
