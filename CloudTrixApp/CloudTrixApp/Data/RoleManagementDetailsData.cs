using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;
using System.Collections.Generic;

namespace CloudTrixApp.Data
{
    public class RoleManagementDetailsData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementDetailsSelectAll]";
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

        public static DataTable Search(string sField, string sCondition, string sValue)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementDetailsSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Role Management Details I D")
            {
                selectCommand.Parameters.AddWithValue("@RoleManagementDetailsID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@RoleManagementDetailsID", DBNull.Value);
            }
            if (sField == "Add Permission")
            {
                selectCommand.Parameters.AddWithValue("@AddPermission", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@AddPermission", DBNull.Value);
            }
            if (sField == "Update Permission")
            {
                selectCommand.Parameters.AddWithValue("@UpdatePermission", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@UpdatePermission", DBNull.Value);
            }
            if (sField == "Delete Permission")
            {
                selectCommand.Parameters.AddWithValue("@DeletePermission", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@DeletePermission", DBNull.Value);
            }
            if (sField == "View Permission")
            {
                selectCommand.Parameters.AddWithValue("@ViewPermission", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ViewPermission", DBNull.Value);
            }
            if (sField == "Role I D")
            {
                selectCommand.Parameters.AddWithValue("@RoleID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@RoleID", DBNull.Value);
            }
            if (sField == "Form I D")
            {
                selectCommand.Parameters.AddWithValue("@FormName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@FormName", DBNull.Value);
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

        public static RoleManagementDetails Select_Record(RoleManagementDetails RoleManagementDetailsPara)
        {
            RoleManagementDetails RoleManagementDetails = new RoleManagementDetails();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementDetailsSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@RoleManagementDetailsID", RoleManagementDetailsPara.RoleManagementDetailsID);
            selectCommand.Parameters.AddWithValue("@RoleID", RoleManagementDetailsPara.RoleID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    RoleManagementDetails.RoleManagementDetailsID = System.Convert.ToInt32(reader["RoleManagementDetailsID"]);
                    RoleManagementDetails.AddPermission = System.Convert.ToBoolean(reader["AddPermission"]);
                    RoleManagementDetails.UpdatePermission = System.Convert.ToBoolean(reader["UpdatePermission"]);
                    RoleManagementDetails.DeletePermission = System.Convert.ToBoolean(reader["DeletePermission"]);
                    RoleManagementDetails.ViewPermission = System.Convert.ToBoolean(reader["ViewPermission"]);
                    RoleManagementDetails.RoleID = reader["RoleID"] is DBNull ? null : (Int32?)reader["RoleID"];
                    RoleManagementDetails.FormID = reader["FormID"] is DBNull ? null : (Int32?)reader["FormID"];
                }
                else
                {
                    RoleManagementDetails = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return RoleManagementDetails;
            }
            finally
            {
                connection.Close();
            }
            return RoleManagementDetails;
        }

        public static bool Add(RoleManagementDetails RoleManagementDetails)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[RoleManagementDetailsInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@RoleID", RoleManagementDetails.RoleID);
            insertCommand.Parameters.AddWithValue("@RoleManagementDetailsID", RoleManagementDetails.RoleManagementDetailsID);
            if (RoleManagementDetails.FormID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@FormID", RoleManagementDetails.FormID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@FormID", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@AddPermission", RoleManagementDetails.AddPermission);
            insertCommand.Parameters.AddWithValue("@UpdatePermission", RoleManagementDetails.UpdatePermission);
            insertCommand.Parameters.AddWithValue("@DeletePermission", RoleManagementDetails.DeletePermission);
            insertCommand.Parameters.AddWithValue("@ViewPermission", RoleManagementDetails.ViewPermission);
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

        public static bool Update(RoleManagementDetails oldRoleManagementDetails, RoleManagementDetails newRoleManagementDetails)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[RoleManagementDetailsUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            //  updateCommand.Parameters.AddWithValue("@NewRoleManagementDetailsID", newRoleManagementDetails.RoleManagementDetailsID);
            updateCommand.Parameters.AddWithValue("@NewAddPermission", newRoleManagementDetails.AddPermission);
            updateCommand.Parameters.AddWithValue("@NewUpdatePermission", newRoleManagementDetails.UpdatePermission);
            updateCommand.Parameters.AddWithValue("@NewDeletePermission", newRoleManagementDetails.DeletePermission);
            updateCommand.Parameters.AddWithValue("@NewViewPermission", newRoleManagementDetails.ViewPermission);
            //if (newRoleManagementDetails.RoleID.HasValue == true) {
            //    updateCommand.Parameters.AddWithValue("@NewRoleID", newRoleManagementDetails.RoleID);
            //} else {
            //    updateCommand.Parameters.AddWithValue("@NewRoleID", DBNull.Value); }
            //if (newRoleManagementDetails.FormID.HasValue == true) {
            //    updateCommand.Parameters.AddWithValue("@NewFormID", newRoleManagementDetails.FormID);
            //} else {
            //    updateCommand.Parameters.AddWithValue("@NewFormID", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldRoleManagementDetailsID", oldRoleManagementDetails.RoleManagementDetailsID);
            //updateCommand.Parameters.AddWithValue("@OldAddPermission", oldRoleManagementDetails.AddPermission);
            //updateCommand.Parameters.AddWithValue("@OldUpdatePermission", oldRoleManagementDetails.UpdatePermission);
            //updateCommand.Parameters.AddWithValue("@OldDeletePermission", oldRoleManagementDetails.DeletePermission);
            //updateCommand.Parameters.AddWithValue("@OldViewPermission", oldRoleManagementDetails.ViewPermission);
            if (oldRoleManagementDetails.RoleID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldRoleID", oldRoleManagementDetails.RoleID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldRoleID", DBNull.Value);
            }
            if (oldRoleManagementDetails.FormID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldFormID", oldRoleManagementDetails.FormID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldFormID", DBNull.Value);
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

        public static bool Delete(RoleManagementDetails RoleManagementDetails)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[RoleManagementDetailsDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldRoleManagementDetailsID", RoleManagementDetails.RoleManagementDetailsID);
            deleteCommand.Parameters.AddWithValue("@OldAddPermission", RoleManagementDetails.AddPermission);
            deleteCommand.Parameters.AddWithValue("@OldUpdatePermission", RoleManagementDetails.UpdatePermission);
            deleteCommand.Parameters.AddWithValue("@OldDeletePermission", RoleManagementDetails.DeletePermission);
            deleteCommand.Parameters.AddWithValue("@OldViewPermission", RoleManagementDetails.ViewPermission);
            if (RoleManagementDetails.RoleID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldRoleID", RoleManagementDetails.RoleID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldRoleID", DBNull.Value);
            }
            if (RoleManagementDetails.FormID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldFormID", RoleManagementDetails.FormID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldFormID", DBNull.Value);
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
        public static List<RoleManagementDetails> List(RoleManagementDetails RoleManagementDetailsPara)
        {
            List<RoleManagementDetails> RoleManagementDetailsList = new List<RoleManagementDetails>();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[RoleManagementDetailsSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@RoleID", RoleManagementDetailsPara.RoleID);
            selectCommand.Parameters.AddWithValue("@RoleManagementDetailsID", RoleManagementDetailsPara.RoleManagementDetailsID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                RoleManagementDetails RoleManagementDetails = new RoleManagementDetails();
                while (reader.Read())
                {
                    RoleManagementDetails = new RoleManagementDetails();
                    RoleManagementDetails.RoleID = System.Convert.ToInt32(reader["RoleID"]);
                    RoleManagementDetails.RoleManagementDetailsID = System.Convert.ToInt32(reader["RoleManagementDetailsID"]);
                    RoleManagementDetails.FormID = System.Convert.ToInt32(reader["FormID"]);
                    RoleManagementDetails.AddPermission = System.Convert.ToBoolean(reader["AddPermission"]);
                    RoleManagementDetails.UpdatePermission = System.Convert.ToBoolean(reader["UpdatePermission"]);
                    RoleManagementDetails.DeletePermission = System.Convert.ToBoolean(reader["DeletePermission"]);
                    RoleManagementDetails.ViewPermission = System.Convert.ToBoolean(reader["ViewPermission"]);
                    RoleManagementDetailsList.Add(RoleManagementDetails);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                return RoleManagementDetailsList;
            }
            finally
            {
                connection.Close();
            }
            return RoleManagementDetailsList;
        }
    }
}