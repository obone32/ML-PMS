using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class RoleManagement_UserTypeData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagement_UserTypeSelect]";
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

        public static List<UserType> List()
        {
            List<UserType> UserTypeList = new List<UserType>();
            SqlConnection connection = PMMSData.GetConnection();
            String selectProcedure = "[UserTypeSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                UserType UserType = new UserType();
                while (reader.Read())
                {
                    UserType = new UserType();
                    UserType.UserTypeID = System.Convert.ToInt32(reader["UserTypeID"]);
                    UserType.UserTypeName = Convert.ToString(reader["UserTypeName"]);
                    UserTypeList.Add(UserType);
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return UserTypeList;
            }
            finally
            {
                connection.Close();
            }
            return UserTypeList;
        }
    }
}