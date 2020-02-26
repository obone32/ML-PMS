using CloudTrixApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

        public static DataTable SelectRoleManagementData(int RoleID)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementDetailsDataSelectAll]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@RoleID", RoleID);
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

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Role I D")
            {
                selectCommand.Parameters.AddWithValue("@RoleID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@RoleID", DBNull.Value);
            }
            if (sField == "Add User I D")
            {
                selectCommand.Parameters.AddWithValue("@AddUserID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@AddUserID", DBNull.Value);
            }
            if (sField == "Add Date")
            {
                selectCommand.Parameters.AddWithValue("@AddDate", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@AddDate", DBNull.Value);
            }
            if (sField == "Archive User I D")
            {
                selectCommand.Parameters.AddWithValue("@ArchiveUserID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value);
            }
            if (sField == "Archive Date")
            {
                selectCommand.Parameters.AddWithValue("@ArchiveDate", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ArchiveDate", DBNull.Value);
            }
            if (sField == "User Type I D")
            {
                selectCommand.Parameters.AddWithValue("@UserTypeName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@UserTypeName", DBNull.Value);
            }
            selectCommand.Parameters.AddWithValue("@SearchCondition", sCondition);
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

        public static RoleManagement Select_Record(RoleManagement RoleManagementPara)
        {
            RoleManagement RoleManagement = new RoleManagement();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@RoleID", RoleManagementPara.RoleID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    RoleManagement.RoleID = System.Convert.ToInt32(reader["RoleID"]);
                    RoleManagement.Description = reader["Description"] is DBNull ? null : (string)reader["Description"];
                    //RoleManagement.AddUserID = reader["AddUserID"] is DBNull ? null : (Int32?)reader["AddUserID"];
                    //RoleManagement.AddDate = reader["AddDate"] is DBNull ? null : (DateTime?)reader["AddDate"];
                    //RoleManagement.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    //RoleManagement.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                    RoleManagement.UserTypeID = reader["UserTypeID"] is DBNull ? null : (Int32?)reader["UserTypeID"];
                }
                else
                {
                    RoleManagement = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return RoleManagement;
            }
            finally
            {
                connection.Close();
            }
            return RoleManagement;
        }

        public static int Add(RoleManagement RoleManagement)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[RoleManagementInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            if (RoleManagement.UserTypeID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@UserTypeID", RoleManagement.UserTypeID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@UserTypeID", DBNull.Value);
            }
            if (RoleManagement.Description != null)
            {
                insertCommand.Parameters.AddWithValue("@Description", RoleManagement.Description);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Description", DBNull.Value);
            }
            insertCommand.Parameters.Add("@RoleID", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@RoleID"].Direction = ParameterDirection.Output;

            //DataTable dtForm = new DataTable();
            //dtForm = FormsData.SelectAll();
            //List<Forms> FormDetails = new List<Forms>();
            //FormDetails = ConvertDataTable<Forms>(dtForm);

            //foreach (var item in FormDetails)
            //{
            //    insertCommand.Parameters.AddWithValue("@RoleID", RoleManagement.RoleID);
            //    insertCommand.Parameters.AddWithValue("@FormID", item.FormID);
            //    //insertCommand.Parameters.AddWithValue("@AddPermission", RoleManagement.RoleManagementDetails.AddPermission);
            //    //insertCommand.Parameters.AddWithValue("@UpdatePermission", RoleManagement.RoleManagementDetails.UpdatePermission);
            //    //insertCommand.Parameters.AddWithValue("@DeletePermission", RoleManagement.RoleManagementDetails.DeletePermission);
            //    //insertCommand.Parameters.AddWithValue("@ViewPermission", RoleManagement.RoleManagementDetails.ViewPermission);

            //    //RoleManagement.RoleManagementDetails.AddPermission = data["AddPermission"]
            //    //insertCommand.Parameters.AddWithValue("@RoleID", RoleManagement.RoleID);
            //    //insertCommand.Parameters.AddWithValue("@FormID", RoleManagement.RoleManagementDetails.FormID);
            //    //insertCommand.Parameters.AddWithValue("@AddPermission", RoleManagement.RoleManagementDetails.AddPermission);
            //    //insertCommand.Parameters.AddWithValue("@UpdatePermission", RoleManagement.RoleManagementDetails.UpdatePermission);
            //    //insertCommand.Parameters.AddWithValue("@DeletePermission", RoleManagement.RoleManagementDetails.DeletePermission);
            //    //insertCommand.Parameters.AddWithValue("@ViewPermission", RoleManagement.RoleManagementDetails.ViewPermission);
            //}
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@RoleID"].Value);
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (SqlException)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(RoleManagement oldRoleManagement,
               RoleManagement newRoleManagement)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[RoleManagementUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            //if (newRoleManagement.AddUserID.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewAddUserID", newRoleManagement.AddUserID);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewAddUserID", DBNull.Value);
            //}
            //if (newRoleManagement.AddDate.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewAddDate", newRoleManagement.AddDate);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewAddDate", DBNull.Value);
            //}
            //if (newRoleManagement.ArchiveUserID.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewArchiveUserID", newRoleManagement.ArchiveUserID);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewArchiveUserID", DBNull.Value);
            //}
            //if (newRoleManagement.ArchiveDate.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewArchiveDate", newRoleManagement.ArchiveDate);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewArchiveDate", DBNull.Value);
            //}

            //if (oldRoleManagement.AddUserID.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@OldAddUserID", oldRoleManagement.AddUserID);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@OldAddUserID", DBNull.Value);
            //}
            //if (oldRoleManagement.AddDate.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@OldAddDate", oldRoleManagement.AddDate);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@OldAddDate", DBNull.Value);
            //}
            //if (oldRoleManagement.ArchiveUserID.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@OldArchiveUserID", oldRoleManagement.ArchiveUserID);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value);
            //}
            //if (oldRoleManagement.ArchiveDate.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@OldArchiveDate", oldRoleManagement.ArchiveDate);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@OldArchiveDate", DBNull.Value);
            //}
            //if (newRoleManagement.UserTypeID.HasValue == true)
            //{
            //    updateCommand.Parameters.AddWithValue("@NewUserTypeID", newRoleManagement.UserTypeID);
            //}
            //else
            //{
            //    updateCommand.Parameters.AddWithValue("@NewUserTypeID", DBNull.Value);
            //}
            updateCommand.Parameters.AddWithValue("@OldRoleID", oldRoleManagement.RoleID);
            if (newRoleManagement.Description != null)
            {
                updateCommand.Parameters.AddWithValue("@NewDescription", newRoleManagement.Description);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewDescription", DBNull.Value);
            }
            if (oldRoleManagement.UserTypeID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldUserTypeID", oldRoleManagement.UserTypeID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldUserTypeID", DBNull.Value);
            }
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

        public static bool Delete(RoleManagement RoleManagement)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[RoleManagementDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldRoleID", RoleManagement.RoleID);
            if (RoleManagement.AddUserID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldAddUserID", RoleManagement.AddUserID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldAddUserID", DBNull.Value);
            }
            if (RoleManagement.AddDate.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", RoleManagement.AddDate);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", DBNull.Value);
            }
            if (RoleManagement.ArchiveUserID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", RoleManagement.ArchiveUserID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value);
            }
            if (RoleManagement.ArchiveDate.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", RoleManagement.ArchiveDate);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", DBNull.Value);
            }
            if (RoleManagement.UserTypeID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldUserTypeID", RoleManagement.UserTypeID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldUserTypeID", DBNull.Value);
            }
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

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}