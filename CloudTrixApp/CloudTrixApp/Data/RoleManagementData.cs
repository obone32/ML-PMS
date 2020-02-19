using CloudTrixApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CloudTrixApp.Data
{
    public class RoleManagementData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
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

        public static bool Add(RoleManagement RoleManagement)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[RoleManagementInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            //insertCommand.Parameters.AddWithValue("@RoleID", RoleManagement.RoleID);
            if (RoleManagement.UserTypeID != null)
            {
                insertCommand.Parameters.AddWithValue("@UserTypeID", RoleManagement.UserTypeID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserTypeID", DBNull.Value);
            }
            if (RoleManagement.AddUserID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@AddUserID", RoleManagement.AddUserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@AddUserID", DBNull.Value);
            }
            if (RoleManagement.AddDate.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@AddDate", RoleManagement.AddDate);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@AddDate", DBNull.Value);
            }
            if (RoleManagement.ArchiveUserID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", RoleManagement.ArchiveUserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value);
            }
            if (RoleManagement.ArchiveDate.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", RoleManagement.ArchiveDate);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", DBNull.Value);
            }
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
    }
}