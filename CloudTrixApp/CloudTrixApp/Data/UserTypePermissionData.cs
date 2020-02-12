using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class UserTypePermissionData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[UserTypePermissionSelectAll]";
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
            string selectProcedure = "[UserTypePermissionSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "User Type I D") {
                selectCommand.Parameters.AddWithValue("@UserTypeName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserTypeName", DBNull.Value); }
            if (sField == "User Type Permission I D") {
                selectCommand.Parameters.AddWithValue("@UserTypePermissionID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserTypePermissionID", DBNull.Value); }
            if (sField == "Permission I D") {
                selectCommand.Parameters.AddWithValue("@PermissionName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@PermissionName", DBNull.Value); }
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

        public static UserTypePermission Select_Record(UserTypePermission UserTypePermissionPara)
        {
            UserTypePermission UserTypePermission = new UserTypePermission();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[UserTypePermissionSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@UserTypeID", UserTypePermissionPara.UserTypeID);
            selectCommand.Parameters.AddWithValue("@UserTypePermissionID", UserTypePermissionPara.UserTypePermissionID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    UserTypePermission.UserTypeID = System.Convert.ToInt32(reader["UserTypeID"]);
                    UserTypePermission.UserTypePermissionID = System.Convert.ToInt32(reader["UserTypePermissionID"]);
                    UserTypePermission.PermissionID = System.Convert.ToInt32(reader["PermissionID"]);
                }
                else
                {
                    UserTypePermission = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return UserTypePermission;
            }
            finally
            {
                connection.Close();
            }
            return UserTypePermission;
        }

        public static bool Add(UserTypePermission UserTypePermission)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[UserTypePermissionInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@UserTypeID", UserTypePermission.UserTypeID);
            insertCommand.Parameters.AddWithValue("@UserTypePermissionID", UserTypePermission.UserTypePermissionID);
            insertCommand.Parameters.AddWithValue("@PermissionID", UserTypePermission.PermissionID);
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

        public static bool Update(UserTypePermission oldUserTypePermission, 
               UserTypePermission newUserTypePermission)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[UserTypePermissionUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewUserTypeID", newUserTypePermission.UserTypeID);
            updateCommand.Parameters.AddWithValue("@NewUserTypePermissionID", newUserTypePermission.UserTypePermissionID);
            updateCommand.Parameters.AddWithValue("@NewPermissionID", newUserTypePermission.PermissionID);
            updateCommand.Parameters.AddWithValue("@OldUserTypeID", oldUserTypePermission.UserTypeID);
            updateCommand.Parameters.AddWithValue("@OldUserTypePermissionID", oldUserTypePermission.UserTypePermissionID);
            updateCommand.Parameters.AddWithValue("@OldPermissionID", oldUserTypePermission.PermissionID);
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

        public static bool Delete(UserTypePermission UserTypePermission)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[UserTypePermissionDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldUserTypeID", UserTypePermission.UserTypeID);
            deleteCommand.Parameters.AddWithValue("@OldUserTypePermissionID", UserTypePermission.UserTypePermissionID);
            deleteCommand.Parameters.AddWithValue("@OldPermissionID", UserTypePermission.PermissionID);
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
 
