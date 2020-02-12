using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class UserTypeData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[UserTypeSelectAll]";
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
            string selectProcedure = "[UserTypeSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "User Type I D") {
                selectCommand.Parameters.AddWithValue("@UserTypeID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserTypeID", DBNull.Value); }
            if (sField == "User Type Name") {
                selectCommand.Parameters.AddWithValue("@UserTypeName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@UserTypeName", DBNull.Value); }
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

        public static UserType Select_Record(UserType UserTypePara)
        {
            UserType UserType = new UserType();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[UserTypeSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@UserTypeID", UserTypePara.UserTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    UserType.UserTypeID = System.Convert.ToInt32(reader["UserTypeID"]);
                    UserType.UserTypeName = System.Convert.ToString(reader["UserTypeName"]);
                }
                else
                {
                    UserType = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return UserType;
            }
            finally
            {
                connection.Close();
            }
            return UserType;
        }

        public static bool Add(UserType UserType)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[UserTypeInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@UserTypeName", UserType.UserTypeName);
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

        public static bool Update(UserType oldUserType, 
               UserType newUserType)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[UserTypeUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewUserTypeName", newUserType.UserTypeName);
            updateCommand.Parameters.AddWithValue("@OldUserTypeID", oldUserType.UserTypeID);
            updateCommand.Parameters.AddWithValue("@OldUserTypeName", oldUserType.UserTypeName);
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

        public static bool Delete(UserType UserType)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[UserTypeDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldUserTypeID", UserType.UserTypeID);
            deleteCommand.Parameters.AddWithValue("@OldUserTypeName", UserType.UserTypeName);
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
 
