using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class RoleManagementDetailsData
    {
        public static bool Add(RoleManagementDetails RoleManagementDetails)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[RoleManagementDetailsInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            //insertCommand.Parameters.AddWithValue("@RoleManagementDetailsID", RoleManagementDetails.RoleManagementDetailsID);
            insertCommand.Parameters.AddWithValue("@RoleID", RoleManagementDetails.RoleID);
            insertCommand.Parameters.AddWithValue("@FormID", RoleManagementDetails.FormID);
            insertCommand.Parameters.AddWithValue("@Quantity", RoleManagementDetails.AddPermission);
            insertCommand.Parameters.AddWithValue("@Rate", RoleManagementDetails.UpdatePermission);
            insertCommand.Parameters.AddWithValue("@DiscountAmount", RoleManagementDetails.DeletePermission);
            insertCommand.Parameters.AddWithValue("@CGSTRate", RoleManagementDetails.ViewPermission);        
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