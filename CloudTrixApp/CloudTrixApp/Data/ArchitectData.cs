using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class ArchitectData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ArchitectSelectAll]";
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
            string selectProcedure = "[ArchitectSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Architect I D") {
                selectCommand.Parameters.AddWithValue("@ArchitectID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ArchitectID", DBNull.Value); }
            if (sField == "Architect Name") {
                selectCommand.Parameters.AddWithValue("@ArchitectName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ArchitectName", DBNull.Value); }
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
            if (sField == "Country") {
                selectCommand.Parameters.AddWithValue("@Country", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Country", DBNull.Value); }
            if (sField == "Pincode") {
                selectCommand.Parameters.AddWithValue("@Pincode", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Pincode", DBNull.Value); }
            if (sField == "E Mail") {
                selectCommand.Parameters.AddWithValue("@EMail", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@EMail", DBNull.Value); }
            if (sField == "Contact No") {
                selectCommand.Parameters.AddWithValue("@ContactNo", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ContactNo", DBNull.Value); }
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

        public static Architect Select_Record(Architect ArchitectPara)
        {
            Architect Architect = new Architect();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ArchitectSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ArchitectID", ArchitectPara.ArchitectID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Architect.ArchitectID = System.Convert.ToInt32(reader["ArchitectID"]);
                    Architect.ArchitectName = System.Convert.ToString(reader["ArchitectName"]);
                    Architect.Address1 = reader["Address1"] is DBNull ? null : reader["Address1"].ToString();
                    Architect.Address2 = reader["Address2"] is DBNull ? null : reader["Address2"].ToString();
                    Architect.City = reader["City"] is DBNull ? null : reader["City"].ToString();
                    Architect.District = reader["District"] is DBNull ? null : reader["District"].ToString();
                    Architect.State = reader["State"] is DBNull ? null : reader["State"].ToString();
                    Architect.Country = reader["Country"] is DBNull ? null : reader["Country"].ToString();
                    Architect.Pincode = reader["Pincode"] is DBNull ? null : reader["Pincode"].ToString();
                    Architect.EMail = reader["EMail"] is DBNull ? null : reader["EMail"].ToString();
                    Architect.ContactNo = reader["ContactNo"] is DBNull ? null : reader["ContactNo"].ToString();
                    Architect.CompanyID = System.Convert.ToInt32(reader["CompanyID"]);
                    Architect.AddUserID = reader["AddUserID"] is DBNull ? null : (Int32?)reader["AddUserID"];
                    Architect.AddDate = reader["AddDate"] is DBNull ? null : (DateTime?)reader["AddDate"];
                    Architect.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    Architect.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                }
                else
                {
                    Architect = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Architect;
            }
            finally
            {
                connection.Close();
            }
            return Architect;
        }

        public static bool Add(Architect Architect)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[ArchitectInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@ArchitectName", Architect.ArchitectName);
            if (Architect.Address1 != null) {
                insertCommand.Parameters.AddWithValue("@Address1", Architect.Address1);
            } else {
                insertCommand.Parameters.AddWithValue("@Address1", DBNull.Value); }
            if (Architect.Address2 != null) {
                insertCommand.Parameters.AddWithValue("@Address2", Architect.Address2);
            } else {
                insertCommand.Parameters.AddWithValue("@Address2", DBNull.Value); }
            if (Architect.City != null) {
                insertCommand.Parameters.AddWithValue("@City", Architect.City);
            } else {
                insertCommand.Parameters.AddWithValue("@City", DBNull.Value); }
            if (Architect.District != null) {
                insertCommand.Parameters.AddWithValue("@District", Architect.District);
            } else {
                insertCommand.Parameters.AddWithValue("@District", DBNull.Value); }
            if (Architect.State != null) {
                insertCommand.Parameters.AddWithValue("@State", Architect.State);
            } else {
                insertCommand.Parameters.AddWithValue("@State", DBNull.Value); }
            if (Architect.Country != null) {
                insertCommand.Parameters.AddWithValue("@Country", Architect.Country);
            } else {
                insertCommand.Parameters.AddWithValue("@Country", DBNull.Value); }
            if (Architect.Pincode != null) {
                insertCommand.Parameters.AddWithValue("@Pincode", Architect.Pincode);
            } else {
                insertCommand.Parameters.AddWithValue("@Pincode", DBNull.Value); }
            if (Architect.EMail != null) {
                insertCommand.Parameters.AddWithValue("@EMail", Architect.EMail);
            } else {
                insertCommand.Parameters.AddWithValue("@EMail", DBNull.Value); }
            if (Architect.ContactNo != null) {
                insertCommand.Parameters.AddWithValue("@ContactNo", Architect.ContactNo);
            } else {
                insertCommand.Parameters.AddWithValue("@ContactNo", DBNull.Value); }
            insertCommand.Parameters.AddWithValue("@CompanyID", Architect.CompanyID);
            if (Architect.AddUserID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@AddUserID", Architect.AddUserID);
            } else {
                insertCommand.Parameters.AddWithValue("@AddUserID", DBNull.Value); }
            if (Architect.AddDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@AddDate", Architect.AddDate);
            } else {
                insertCommand.Parameters.AddWithValue("@AddDate", DBNull.Value); }
            if (Architect.ArchiveUserID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", Architect.ArchiveUserID);
            } else {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value); }
            if (Architect.ArchiveDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", Architect.ArchiveDate);
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

        public static bool Update(Architect oldArchitect, 
               Architect newArchitect)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[ArchitectUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewArchitectName", newArchitect.ArchitectName);
            if (newArchitect.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress1", newArchitect.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress1", DBNull.Value); }
            if (newArchitect.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress2", newArchitect.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress2", DBNull.Value); }
            if (newArchitect.City != null) {
                updateCommand.Parameters.AddWithValue("@NewCity", newArchitect.City);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCity", DBNull.Value); }
            if (newArchitect.District != null) {
                updateCommand.Parameters.AddWithValue("@NewDistrict", newArchitect.District);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDistrict", DBNull.Value); }
            if (newArchitect.State != null) {
                updateCommand.Parameters.AddWithValue("@NewState", newArchitect.State);
            } else {
                updateCommand.Parameters.AddWithValue("@NewState", DBNull.Value); }
            if (newArchitect.Country != null) {
                updateCommand.Parameters.AddWithValue("@NewCountry", newArchitect.Country);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCountry", DBNull.Value); }
            if (newArchitect.Pincode != null) {
                updateCommand.Parameters.AddWithValue("@NewPincode", newArchitect.Pincode);
            } else {
                updateCommand.Parameters.AddWithValue("@NewPincode", DBNull.Value); }
            if (newArchitect.EMail != null) {
                updateCommand.Parameters.AddWithValue("@NewEMail", newArchitect.EMail);
            } else {
                updateCommand.Parameters.AddWithValue("@NewEMail", DBNull.Value); }
            if (newArchitect.ContactNo != null) {
                updateCommand.Parameters.AddWithValue("@NewContactNo", newArchitect.ContactNo);
            } else {
                updateCommand.Parameters.AddWithValue("@NewContactNo", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@NewCompanyID", newArchitect.CompanyID);
            if (newArchitect.AddUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewAddUserID", newArchitect.AddUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddUserID", DBNull.Value); }
            if (newArchitect.AddDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewAddDate", newArchitect.AddDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddDate", DBNull.Value); }
            if (newArchitect.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", newArchitect.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", DBNull.Value); }
            if (newArchitect.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", newArchitect.ArchiveDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldArchitectID", oldArchitect.ArchitectID);
            updateCommand.Parameters.AddWithValue("@OldArchitectName", oldArchitect.ArchitectName);
            if (oldArchitect.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress1", oldArchitect.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (oldArchitect.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress2", oldArchitect.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (oldArchitect.City != null) {
                updateCommand.Parameters.AddWithValue("@OldCity", oldArchitect.City);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (oldArchitect.District != null) {
                updateCommand.Parameters.AddWithValue("@OldDistrict", oldArchitect.District);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value); }
            if (oldArchitect.State != null) {
                updateCommand.Parameters.AddWithValue("@OldState", oldArchitect.State);
            } else {
                updateCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (oldArchitect.Country != null) {
                updateCommand.Parameters.AddWithValue("@OldCountry", oldArchitect.Country);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCountry", DBNull.Value); }
            if (oldArchitect.Pincode != null) {
                updateCommand.Parameters.AddWithValue("@OldPincode", oldArchitect.Pincode);
            } else {
                updateCommand.Parameters.AddWithValue("@OldPincode", DBNull.Value); }
            if (oldArchitect.EMail != null) {
                updateCommand.Parameters.AddWithValue("@OldEMail", oldArchitect.EMail);
            } else {
                updateCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value); }
            if (oldArchitect.ContactNo != null) {
                updateCommand.Parameters.AddWithValue("@OldContactNo", oldArchitect.ContactNo);
            } else {
                updateCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldArchitect.CompanyID);
            if (oldArchitect.AddUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldAddUserID", oldArchitect.AddUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddUserID", DBNull.Value); }
            if (oldArchitect.AddDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldAddDate", oldArchitect.AddDate);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddDate", DBNull.Value); }
            if (oldArchitect.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", oldArchitect.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (oldArchitect.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", oldArchitect.ArchiveDate);
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

        public static bool Delete(Architect Architect)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[ArchitectDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldArchitectID", Architect.ArchitectID);
            deleteCommand.Parameters.AddWithValue("@OldArchitectName", Architect.ArchitectName);
            if (Architect.Address1 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", Architect.Address1);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (Architect.Address2 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", Architect.Address2);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (Architect.City != null) {
                deleteCommand.Parameters.AddWithValue("@OldCity", Architect.City);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (Architect.District != null) {
                deleteCommand.Parameters.AddWithValue("@OldDistrict", Architect.District);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value); }
            if (Architect.State != null) {
                deleteCommand.Parameters.AddWithValue("@OldState", Architect.State);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (Architect.Country != null) {
                deleteCommand.Parameters.AddWithValue("@OldCountry", Architect.Country);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCountry", DBNull.Value); }
            if (Architect.Pincode != null) {
                deleteCommand.Parameters.AddWithValue("@OldPincode", Architect.Pincode);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldPincode", DBNull.Value); }
            if (Architect.EMail != null) {
                deleteCommand.Parameters.AddWithValue("@OldEMail", Architect.EMail);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value); }
            if (Architect.ContactNo != null) {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", Architect.ContactNo);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value); }
            deleteCommand.Parameters.AddWithValue("@OldCompanyID", Architect.CompanyID);
            if (Architect.AddUserID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldAddUserID", Architect.AddUserID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddUserID", DBNull.Value); }
            if (Architect.AddDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", Architect.AddDate);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", DBNull.Value); }
            if (Architect.ArchiveUserID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", Architect.ArchiveUserID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (Architect.ArchiveDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", Architect.ArchiveDate);
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
 
