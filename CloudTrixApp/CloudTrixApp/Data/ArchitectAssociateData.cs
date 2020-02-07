using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class ArchitectAssociateData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ArchitectAssociateSelectAll]";
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
            string selectProcedure = "[ArchitectAssociateSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Architect Associate I D")
            {
                selectCommand.Parameters.AddWithValue("@ArchitectAssociateID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ArchitectAssociateID", DBNull.Value);
            }
            if (sField == "Architect I D")
            {
                selectCommand.Parameters.AddWithValue("@ArchitectName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ArchitectName", DBNull.Value);
            }
            if (sField == "Associate Name")
            {
                selectCommand.Parameters.AddWithValue("@AssociateName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@AssociateName", DBNull.Value);
            }
            if (sField == "Contact No")
            {
                selectCommand.Parameters.AddWithValue("@ContactNo", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ContactNo", DBNull.Value);
            }
            if (sField == "E Mail")
            {
                selectCommand.Parameters.AddWithValue("@EMail", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@EMail", DBNull.Value);
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

        public static ArchitectAssociate Select_Record(ArchitectAssociate ArchitectAssociatePara)
        {
            ArchitectAssociate ArchitectAssociate = new ArchitectAssociate();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ArchitectAssociateSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ArchitectAssociateID", ArchitectAssociatePara.ArchitectAssociateID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ArchitectAssociate.ArchitectAssociateID = System.Convert.ToInt32(reader["ArchitectAssociateID"]);
                    ArchitectAssociate.ArchitectID = System.Convert.ToInt32(reader["ArchitectID"]);
                    ArchitectAssociate.AssociateName = System.Convert.ToString(reader["AssociateName"]);
                    ArchitectAssociate.ContactNo = reader["ContactNo"] is DBNull ? null : reader["ContactNo"].ToString();
                    ArchitectAssociate.EMail = reader["EMail"] is DBNull ? null : reader["EMail"].ToString();
                }
                else
                {
                    ArchitectAssociate = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return ArchitectAssociate;
            }
            finally
            {
                connection.Close();
            }
            return ArchitectAssociate;
        }

        public static bool Add(ArchitectAssociate ArchitectAssociate)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[ArchitectAssociateInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@ArchitectID", ArchitectAssociate.ArchitectID);
            insertCommand.Parameters.AddWithValue("@AssociateName", ArchitectAssociate.AssociateName);
            if (ArchitectAssociate.ContactNo != null)
            {
                insertCommand.Parameters.AddWithValue("@ContactNo", ArchitectAssociate.ContactNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ContactNo", DBNull.Value);
            }
            if (ArchitectAssociate.EMail != null)
            {
                insertCommand.Parameters.AddWithValue("@EMail", ArchitectAssociate.EMail);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@EMail", DBNull.Value);
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

        public static bool Update(ArchitectAssociate oldArchitectAssociate,
               ArchitectAssociate newArchitectAssociate)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[ArchitectAssociateUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewArchitectID", newArchitectAssociate.ArchitectID);
            updateCommand.Parameters.AddWithValue("@NewAssociateName", newArchitectAssociate.AssociateName);
            if (newArchitectAssociate.ContactNo != null)
            {
                updateCommand.Parameters.AddWithValue("@NewContactNo", newArchitectAssociate.ContactNo);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewContactNo", DBNull.Value);
            }
            if (newArchitectAssociate.EMail != null)
            {
                updateCommand.Parameters.AddWithValue("@NewEMail", newArchitectAssociate.EMail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewEMail", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldArchitectAssociateID", oldArchitectAssociate.ArchitectAssociateID);
            updateCommand.Parameters.AddWithValue("@OldArchitectID", oldArchitectAssociate.ArchitectID);
            updateCommand.Parameters.AddWithValue("@OldAssociateName", oldArchitectAssociate.AssociateName);
            if (oldArchitectAssociate.ContactNo != null)
            {
                updateCommand.Parameters.AddWithValue("@OldContactNo", oldArchitectAssociate.ContactNo);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value);
            }
            if (oldArchitectAssociate.EMail != null)
            {
                updateCommand.Parameters.AddWithValue("@OldEMail", oldArchitectAssociate.EMail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value);
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

        public static bool Delete(ArchitectAssociate ArchitectAssociate)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[ArchitectAssociateDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldArchitectAssociateID", ArchitectAssociate.ArchitectAssociateID);
            deleteCommand.Parameters.AddWithValue("@OldArchitectID", ArchitectAssociate.ArchitectID);
            deleteCommand.Parameters.AddWithValue("@OldAssociateName", ArchitectAssociate.AssociateName);
            if (ArchitectAssociate.ContactNo != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", ArchitectAssociate.ContactNo);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value);
            }
            if (ArchitectAssociate.EMail != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldEMail", ArchitectAssociate.EMail);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value);
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
    }
}

