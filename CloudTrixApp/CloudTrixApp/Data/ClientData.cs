using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class ClientData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ClientSelectAll]";
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
            string selectProcedure = "[ClientSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Client I D") {
                selectCommand.Parameters.AddWithValue("@ClientID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ClientID", DBNull.Value); }
            if (sField == "Client Name") {
                selectCommand.Parameters.AddWithValue("@ClientName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ClientName", DBNull.Value); }
            if (sField == "Address1") {
                selectCommand.Parameters.AddWithValue("@Address1", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Address1", DBNull.Value); }
            if (sField == "Address2") {
                selectCommand.Parameters.AddWithValue("@Address2", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Address2", DBNull.Value); }
            if (sField == "City") {
                selectCommand.Parameters.AddWithValue("@City", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@City", DBNull.Value); }
            if (sField == "District") {
                selectCommand.Parameters.AddWithValue("@District", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@District", DBNull.Value); }
            if (sField == "State") {
                selectCommand.Parameters.AddWithValue("@State", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@State", DBNull.Value); }
            if (sField == "Pin Code") {
                selectCommand.Parameters.AddWithValue("@PinCode", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@PinCode", DBNull.Value); }
            if (sField == "Contact No") {
                selectCommand.Parameters.AddWithValue("@ContactNo", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ContactNo", DBNull.Value); }
            if (sField == "E Mail") {
                selectCommand.Parameters.AddWithValue("@EMail", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@EMail", DBNull.Value); }
            if (sField == "G S T I N") {
                selectCommand.Parameters.AddWithValue("@GSTIN", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@GSTIN", DBNull.Value); }
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

        public static Client Select_Record(Client ClientPara)
        {
            Client Client = new Client();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ClientSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ClientID", ClientPara.ClientID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Client.ClientID = System.Convert.ToInt32(reader["ClientID"]);
                    Client.ClientName = System.Convert.ToString(reader["ClientName"]);
                    Client.Address1 = reader["Address1"] is DBNull ? null : reader["Address1"].ToString();
                    Client.Address2 = reader["Address2"] is DBNull ? null : reader["Address2"].ToString();
                    Client.City = reader["City"] is DBNull ? null : reader["City"].ToString();
                    Client.District = reader["District"] is DBNull ? null : reader["District"].ToString();
                    Client.State = reader["State"] is DBNull ? null : reader["State"].ToString();
                    Client.PinCode = reader["PinCode"] is DBNull ? null : reader["PinCode"].ToString();
                    Client.ContactNo = reader["ContactNo"] is DBNull ? null : reader["ContactNo"].ToString();
                    Client.EMail = reader["EMail"] is DBNull ? null : reader["EMail"].ToString();
                    Client.GSTIN = reader["GSTIN"] is DBNull ? null : reader["GSTIN"].ToString();
                    Client.CompanyID = System.Convert.ToInt32(reader["CompanyID"]);
                    Client.AddUserID = System.Convert.ToInt32(reader["AddUserID"]);
                    Client.AddDate = System.Convert.ToDateTime(reader["AddDate"]);
                    Client.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    Client.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                }
                else
                {
                    Client = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Client;
            }
            finally
            {
                connection.Close();
            }
            return Client;
        }

        public static bool Add(Client Client)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[ClientInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@ClientName", Client.ClientName);
            if (Client.Address1 != null) {
                insertCommand.Parameters.AddWithValue("@Address1", Client.Address1);
            } else {
                insertCommand.Parameters.AddWithValue("@Address1", DBNull.Value); }
            if (Client.Address2 != null) {
                insertCommand.Parameters.AddWithValue("@Address2", Client.Address2);
            } else {
                insertCommand.Parameters.AddWithValue("@Address2", DBNull.Value); }
            if (Client.City != null) {
                insertCommand.Parameters.AddWithValue("@City", Client.City);
            } else {
                insertCommand.Parameters.AddWithValue("@City", DBNull.Value); }
            if (Client.District != null) {
                insertCommand.Parameters.AddWithValue("@District", Client.District);
            } else {
                insertCommand.Parameters.AddWithValue("@District", DBNull.Value); }
            if (Client.State != null) {
                insertCommand.Parameters.AddWithValue("@State", Client.State);
            } else {
                insertCommand.Parameters.AddWithValue("@State", DBNull.Value); }
            if (Client.PinCode != null) {
                insertCommand.Parameters.AddWithValue("@PinCode", Client.PinCode);
            } else {
                insertCommand.Parameters.AddWithValue("@PinCode", DBNull.Value); }
            if (Client.ContactNo != null) {
                insertCommand.Parameters.AddWithValue("@ContactNo", Client.ContactNo);
            } else {
                insertCommand.Parameters.AddWithValue("@ContactNo", DBNull.Value); }
            if (Client.EMail != null) {
                insertCommand.Parameters.AddWithValue("@EMail", Client.EMail);
            } else {
                insertCommand.Parameters.AddWithValue("@EMail", DBNull.Value); }
            if (Client.GSTIN != null) {
                insertCommand.Parameters.AddWithValue("@GSTIN", Client.GSTIN);
            } else {
                insertCommand.Parameters.AddWithValue("@GSTIN", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@CompanyID", Client.CompanyID);
            insertCommand.Parameters.AddWithValue("@AddUserID", Client.AddUserID);
            insertCommand.Parameters.AddWithValue("@AddDate", Client.AddDate);
            if (Client.ArchiveUserID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", Client.ArchiveUserID);
            } else {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value); }
            if (Client.ArchiveDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", Client.ArchiveDate);
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

        public static bool Update(Client oldClient, 
               Client newClient)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[ClientUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewClientName", newClient.ClientName);
            if (newClient.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress1", newClient.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress1", DBNull.Value); }
            if (newClient.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress2", newClient.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress2", DBNull.Value); }
            if (newClient.City != null) {
                updateCommand.Parameters.AddWithValue("@NewCity", newClient.City);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCity", DBNull.Value); }
            if (newClient.District != null) {
                updateCommand.Parameters.AddWithValue("@NewDistrict", newClient.District);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDistrict", DBNull.Value); }
            if (newClient.State != null) {
                updateCommand.Parameters.AddWithValue("@NewState", newClient.State);
            } else {
                updateCommand.Parameters.AddWithValue("@NewState", DBNull.Value); }
            if (newClient.PinCode != null) {
                updateCommand.Parameters.AddWithValue("@NewPinCode", newClient.PinCode);
            } else {
                updateCommand.Parameters.AddWithValue("@NewPinCode", DBNull.Value); }
            if (newClient.ContactNo != null) {
                updateCommand.Parameters.AddWithValue("@NewContactNo", newClient.ContactNo);
            } else {
                updateCommand.Parameters.AddWithValue("@NewContactNo", DBNull.Value); }
            if (newClient.EMail != null) {
                updateCommand.Parameters.AddWithValue("@NewEMail", newClient.EMail);
            } else {
                updateCommand.Parameters.AddWithValue("@NewEMail", DBNull.Value); }
            if (newClient.GSTIN != null) {
                updateCommand.Parameters.AddWithValue("@NewGSTIN", newClient.GSTIN);
            } else {
                updateCommand.Parameters.AddWithValue("@NewGSTIN", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewCompanyID", newClient.CompanyID);
            updateCommand.Parameters.AddWithValue("@NewAddUserID", newClient.AddUserID);
            updateCommand.Parameters.AddWithValue("@NewAddDate", newClient.AddDate);
            if (newClient.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", newClient.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", DBNull.Value); }
            if (newClient.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", newClient.ArchiveDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldClientID", oldClient.ClientID);
            updateCommand.Parameters.AddWithValue("@OldClientName", oldClient.ClientName);
            if (oldClient.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress1", oldClient.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (oldClient.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress2", oldClient.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (oldClient.City != null) {
                updateCommand.Parameters.AddWithValue("@OldCity", oldClient.City);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (oldClient.District != null) {
                updateCommand.Parameters.AddWithValue("@OldDistrict", oldClient.District);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value); }
            if (oldClient.State != null) {
                updateCommand.Parameters.AddWithValue("@OldState", oldClient.State);
            } else {
                updateCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (oldClient.PinCode != null) {
                updateCommand.Parameters.AddWithValue("@OldPinCode", oldClient.PinCode);
            } else {
                updateCommand.Parameters.AddWithValue("@OldPinCode", DBNull.Value); }
            if (oldClient.ContactNo != null) {
                updateCommand.Parameters.AddWithValue("@OldContactNo", oldClient.ContactNo);
            } else {
                updateCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value); }
            if (oldClient.EMail != null) {
                updateCommand.Parameters.AddWithValue("@OldEMail", oldClient.EMail);
            } else {
                updateCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value); }
            if (oldClient.GSTIN != null) {
                updateCommand.Parameters.AddWithValue("@OldGSTIN", oldClient.GSTIN);
            } else {
                updateCommand.Parameters.AddWithValue("@OldGSTIN", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldClient.CompanyID);
            updateCommand.Parameters.AddWithValue("@OldAddUserID", oldClient.AddUserID);
            updateCommand.Parameters.AddWithValue("@OldAddDate", oldClient.AddDate);
            if (oldClient.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", oldClient.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (oldClient.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", oldClient.ArchiveDate);
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

        public static bool Delete(Client Client)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[ClientDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldClientID", Client.ClientID);
            deleteCommand.Parameters.AddWithValue("@OldClientName", Client.ClientName);
            if (Client.Address1 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", Client.Address1);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (Client.Address2 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", Client.Address2);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (Client.City != null) {
                deleteCommand.Parameters.AddWithValue("@OldCity", Client.City);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (Client.District != null) {
                deleteCommand.Parameters.AddWithValue("@OldDistrict", Client.District);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value); }
            if (Client.State != null) {
                deleteCommand.Parameters.AddWithValue("@OldState", Client.State);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (Client.PinCode != null) {
                deleteCommand.Parameters.AddWithValue("@OldPinCode", Client.PinCode);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldPinCode", DBNull.Value); }
            if (Client.ContactNo != null) {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", Client.ContactNo);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value); }
            if (Client.EMail != null) {
                deleteCommand.Parameters.AddWithValue("@OldEMail", Client.EMail);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value); }
            if (Client.GSTIN != null) {
                deleteCommand.Parameters.AddWithValue("@OldGSTIN", Client.GSTIN);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldGSTIN", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldCompanyID", Client.CompanyID);
            deleteCommand.Parameters.AddWithValue("@OldAddUserID", Client.AddUserID);
            deleteCommand.Parameters.AddWithValue("@OldAddDate", Client.AddDate);
            if (Client.ArchiveUserID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", Client.ArchiveUserID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (Client.ArchiveDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", Client.ArchiveDate);
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
 
