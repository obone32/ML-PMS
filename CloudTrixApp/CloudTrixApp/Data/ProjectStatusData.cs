using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class ProjectStatusData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ProjectStatusSelectAll]";
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
            string selectProcedure = "[ProjectStatusSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Project Status I D") {
                selectCommand.Parameters.AddWithValue("@ProjectStatusID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectStatusID", DBNull.Value); }
            if (sField == "Project Status Name") {
                selectCommand.Parameters.AddWithValue("@ProjectStatusName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectStatusName", DBNull.Value); }
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

        public static ProjectStatus Select_Record(ProjectStatus ProjectStatusPara)
        {
            ProjectStatus ProjectStatus = new ProjectStatus();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ProjectStatusSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ProjectStatusID", ProjectStatusPara.ProjectStatusID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ProjectStatus.ProjectStatusID = System.Convert.ToInt32(reader["ProjectStatusID"]);
                    ProjectStatus.ProjectStatusName = System.Convert.ToString(reader["ProjectStatusName"]);
                }
                else
                {
                    ProjectStatus = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ProjectStatus;
            }
            finally
            {
                connection.Close();
            }
            return ProjectStatus;
        }

        public static bool Add(ProjectStatus ProjectStatus)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[ProjectStatusInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@ProjectStatusName", ProjectStatus.ProjectStatusName);
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

        public static bool Update(ProjectStatus oldProjectStatus, 
               ProjectStatus newProjectStatus)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[ProjectStatusUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewProjectStatusName", newProjectStatus.ProjectStatusName);
            updateCommand.Parameters.AddWithValue("@OldProjectStatusID", oldProjectStatus.ProjectStatusID);
            updateCommand.Parameters.AddWithValue("@OldProjectStatusName", oldProjectStatus.ProjectStatusName);
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

        public static bool Delete(ProjectStatus ProjectStatus)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[ProjectStatusDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldProjectStatusID", ProjectStatus.ProjectStatusID);
            deleteCommand.Parameters.AddWithValue("@OldProjectStatusName", ProjectStatus.ProjectStatusName);
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
 
