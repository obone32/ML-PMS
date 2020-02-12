using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class CompanyData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[CompanySelectAll]";
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
            string selectProcedure = "[CompanySearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Company I D") {
                selectCommand.Parameters.AddWithValue("@CompanyID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CompanyID", DBNull.Value); }
            if (sField == "Company Name") {
                selectCommand.Parameters.AddWithValue("@CompanyName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CompanyName", DBNull.Value); }
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
            if (sField == "Invoice Initials") {
                selectCommand.Parameters.AddWithValue("@InvoiceInitials", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@InvoiceInitials", DBNull.Value); }
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

        public static Company Select_Record(Company CompanyPara)
        {
            Company Company = new Company();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[CompanySelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@CompanyID", CompanyPara.CompanyID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Company.CompanyID = System.Convert.ToInt32(reader["CompanyID"]);
                    Company.CompanyName = System.Convert.ToString(reader["CompanyName"]);
                    Company.Address1 = reader["Address1"] is DBNull ? null : reader["Address1"].ToString();
                    Company.Address2 = reader["Address2"] is DBNull ? null : reader["Address2"].ToString();
                    Company.City = reader["City"] is DBNull ? null : reader["City"].ToString();
                    Company.District = reader["District"] is DBNull ? null : reader["District"].ToString();
                    Company.State = reader["State"] is DBNull ? null : reader["State"].ToString();
                    Company.Country = reader["Country"] is DBNull ? null : reader["Country"].ToString();
                    Company.PinCode = reader["PinCode"] is DBNull ? null : reader["PinCode"].ToString();
                    Company.ContactNo = reader["ContactNo"] is DBNull ? null : reader["ContactNo"].ToString();
                    Company.EMail = reader["EMail"] is DBNull ? null : reader["EMail"].ToString();
                    Company.GSTIN = reader["GSTIN"] is DBNull ? null : reader["GSTIN"].ToString();
                    Company.InvoiceInitials = reader["InvoiceInitials"] is DBNull ? null : reader["InvoiceInitials"].ToString();
                    Company.AddUserID = reader["AddUserID"] is DBNull ? null : (Int32?)reader["AddUserID"];
                    Company.AddDate = reader["AddDate"] is DBNull ? null : (DateTime?)reader["AddDate"];
                    Company.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    Company.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                }
                else
                {
                    Company = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Company;
            }
            finally
            {
                connection.Close();
            }
            return Company;
        }

        public static bool Add(Company Company)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[CompanyInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@CompanyName", Company.CompanyName);
            if (Company.Address1 != null) {
                insertCommand.Parameters.AddWithValue("@Address1", Company.Address1);
            } else {
                insertCommand.Parameters.AddWithValue("@Address1", DBNull.Value); }
            if (Company.Address2 != null) {
                insertCommand.Parameters.AddWithValue("@Address2", Company.Address2);
            } else {
                insertCommand.Parameters.AddWithValue("@Address2", DBNull.Value); }
            if (Company.City != null) {
                insertCommand.Parameters.AddWithValue("@City", Company.City);
            } else {
                insertCommand.Parameters.AddWithValue("@City", DBNull.Value); }
            if (Company.District != null) {
                insertCommand.Parameters.AddWithValue("@District", Company.District);
            } else {
                insertCommand.Parameters.AddWithValue("@District", DBNull.Value); }
            if (Company.State != null) {
                insertCommand.Parameters.AddWithValue("@State", Company.State);
            } else {
                insertCommand.Parameters.AddWithValue("@State", DBNull.Value); }
            if (Company.Country != null) {
                insertCommand.Parameters.AddWithValue("@Country", Company.Country);
            } else {
                insertCommand.Parameters.AddWithValue("@Country", DBNull.Value); }
            if (Company.PinCode != null) {
                insertCommand.Parameters.AddWithValue("@PinCode", Company.PinCode);
            } else {
                insertCommand.Parameters.AddWithValue("@PinCode", DBNull.Value); }
            if (Company.ContactNo != null) {
                insertCommand.Parameters.AddWithValue("@ContactNo", Company.ContactNo);
            } else {
                insertCommand.Parameters.AddWithValue("@ContactNo", DBNull.Value); }
            if (Company.EMail != null) {
                insertCommand.Parameters.AddWithValue("@EMail", Company.EMail);
            } else {
                insertCommand.Parameters.AddWithValue("@EMail", DBNull.Value); }
            if (Company.GSTIN != null) {
                insertCommand.Parameters.AddWithValue("@GSTIN", Company.GSTIN);
            } else {
                insertCommand.Parameters.AddWithValue("@GSTIN", DBNull.Value); }
            if (Company.InvoiceInitials != null) {
                insertCommand.Parameters.AddWithValue("@InvoiceInitials", Company.InvoiceInitials);
            } else {
                insertCommand.Parameters.AddWithValue("@InvoiceInitials", DBNull.Value); }
            if (Company.AddUserID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@AddUserID", Company.AddUserID);
            } else {
                insertCommand.Parameters.AddWithValue("@AddUserID", DBNull.Value); }
            if (Company.AddDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@AddDate", Company.AddDate);
            } else {
                insertCommand.Parameters.AddWithValue("@AddDate", DBNull.Value); }
            if (Company.ArchiveUserID.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", Company.ArchiveUserID);
            } else {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value); }
            if (Company.ArchiveDate.HasValue == true) {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", Company.ArchiveDate);
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

        public static bool Update(Company oldCompany, 
               Company newCompany)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[CompanyUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewCompanyName", newCompany.CompanyName);
            if (newCompany.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress1", newCompany.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress1", DBNull.Value); }
            if (newCompany.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@NewAddress2", newCompany.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddress2", DBNull.Value); }
            if (newCompany.City != null) {
                updateCommand.Parameters.AddWithValue("@NewCity", newCompany.City);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCity", DBNull.Value); }
            if (newCompany.District != null) {
                updateCommand.Parameters.AddWithValue("@NewDistrict", newCompany.District);
            } else {
                updateCommand.Parameters.AddWithValue("@NewDistrict", DBNull.Value); }
            if (newCompany.State != null) {
                updateCommand.Parameters.AddWithValue("@NewState", newCompany.State);
            } else {
                updateCommand.Parameters.AddWithValue("@NewState", DBNull.Value); }
            if (newCompany.Country != null) {
                updateCommand.Parameters.AddWithValue("@NewCountry", newCompany.Country);
            } else {
                updateCommand.Parameters.AddWithValue("@NewCountry", DBNull.Value); }
            if (newCompany.PinCode != null) {
                updateCommand.Parameters.AddWithValue("@NewPinCode", newCompany.PinCode);
            } else {
                updateCommand.Parameters.AddWithValue("@NewPinCode", DBNull.Value); }
            if (newCompany.ContactNo != null) {
                updateCommand.Parameters.AddWithValue("@NewContactNo", newCompany.ContactNo);
            } else {
                updateCommand.Parameters.AddWithValue("@NewContactNo", DBNull.Value); }
            if (newCompany.EMail != null) {
                updateCommand.Parameters.AddWithValue("@NewEMail", newCompany.EMail);
            } else {
                updateCommand.Parameters.AddWithValue("@NewEMail", DBNull.Value); }
            if (newCompany.GSTIN != null) {
                updateCommand.Parameters.AddWithValue("@NewGSTIN", newCompany.GSTIN);
            } else {
                updateCommand.Parameters.AddWithValue("@NewGSTIN", DBNull.Value); }
            if (newCompany.InvoiceInitials != null) {
                updateCommand.Parameters.AddWithValue("@NewInvoiceInitials", newCompany.InvoiceInitials);
            } else {
                updateCommand.Parameters.AddWithValue("@NewInvoiceInitials", DBNull.Value); }
            if (newCompany.AddUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewAddUserID", newCompany.AddUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddUserID", DBNull.Value); }
            if (newCompany.AddDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewAddDate", newCompany.AddDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewAddDate", DBNull.Value); }
            if (newCompany.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", newCompany.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", DBNull.Value); }
            if (newCompany.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", newCompany.ArchiveDate);
            } else {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldCompany.CompanyID);
            updateCommand.Parameters.AddWithValue("@OldCompanyName", oldCompany.CompanyName);
            if (oldCompany.Address1 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress1", oldCompany.Address1);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (oldCompany.Address2 != null) {
                updateCommand.Parameters.AddWithValue("@OldAddress2", oldCompany.Address2);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (oldCompany.City != null) {
                updateCommand.Parameters.AddWithValue("@OldCity", oldCompany.City);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (oldCompany.District != null) {
                updateCommand.Parameters.AddWithValue("@OldDistrict", oldCompany.District);
            } else {
                updateCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value); }
            if (oldCompany.State != null) {
                updateCommand.Parameters.AddWithValue("@OldState", oldCompany.State);
            } else {
                updateCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (oldCompany.Country != null) {
                updateCommand.Parameters.AddWithValue("@OldCountry", oldCompany.Country);
            } else {
                updateCommand.Parameters.AddWithValue("@OldCountry", DBNull.Value); }
            if (oldCompany.PinCode != null) {
                updateCommand.Parameters.AddWithValue("@OldPinCode", oldCompany.PinCode);
            } else {
                updateCommand.Parameters.AddWithValue("@OldPinCode", DBNull.Value); }
            if (oldCompany.ContactNo != null) {
                updateCommand.Parameters.AddWithValue("@OldContactNo", oldCompany.ContactNo);
            } else {
                updateCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value); }
            if (oldCompany.EMail != null) {
                updateCommand.Parameters.AddWithValue("@OldEMail", oldCompany.EMail);
            } else {
                updateCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value); }
            if (oldCompany.GSTIN != null) {
                updateCommand.Parameters.AddWithValue("@OldGSTIN", oldCompany.GSTIN);
            } else {
                updateCommand.Parameters.AddWithValue("@OldGSTIN", DBNull.Value); }
            if (oldCompany.InvoiceInitials != null) {
                updateCommand.Parameters.AddWithValue("@OldInvoiceInitials", oldCompany.InvoiceInitials);
            } else {
                updateCommand.Parameters.AddWithValue("@OldInvoiceInitials", DBNull.Value); }
            if (oldCompany.AddUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldAddUserID", oldCompany.AddUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddUserID", DBNull.Value); }
            if (oldCompany.AddDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldAddDate", oldCompany.AddDate);
            } else {
                updateCommand.Parameters.AddWithValue("@OldAddDate", DBNull.Value); }
            if (oldCompany.ArchiveUserID.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", oldCompany.ArchiveUserID);
            } else {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (oldCompany.ArchiveDate.HasValue == true) {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", oldCompany.ArchiveDate);
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

        public static bool Delete(Company Company)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[CompanyDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldCompanyID", Company.CompanyID);
            deleteCommand.Parameters.AddWithValue("@OldCompanyName", Company.CompanyName);
            if (Company.Address1 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", Company.Address1);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value); }
            if (Company.Address2 != null) {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", Company.Address2);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value); }
            if (Company.City != null) {
                deleteCommand.Parameters.AddWithValue("@OldCity", Company.City);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCity", DBNull.Value); }
            if (Company.District != null) {
                deleteCommand.Parameters.AddWithValue("@OldDistrict", Company.District);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldDistrict", DBNull.Value); }
            if (Company.State != null) {
                deleteCommand.Parameters.AddWithValue("@OldState", Company.State);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldState", DBNull.Value); }
            if (Company.Country != null) {
                deleteCommand.Parameters.AddWithValue("@OldCountry", Company.Country);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldCountry", DBNull.Value); }
            if (Company.PinCode != null) {
                deleteCommand.Parameters.AddWithValue("@OldPinCode", Company.PinCode);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldPinCode", DBNull.Value); }
            if (Company.ContactNo != null) {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", Company.ContactNo);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldContactNo", DBNull.Value); }
            if (Company.EMail != null) {
                deleteCommand.Parameters.AddWithValue("@OldEMail", Company.EMail);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value); }
            if (Company.GSTIN != null) {
                deleteCommand.Parameters.AddWithValue("@OldGSTIN", Company.GSTIN);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldGSTIN", DBNull.Value); }
            if (Company.InvoiceInitials != null) {
                deleteCommand.Parameters.AddWithValue("@OldInvoiceInitials", Company.InvoiceInitials);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldInvoiceInitials", DBNull.Value); }
            if (Company.AddUserID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldAddUserID", Company.AddUserID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddUserID", DBNull.Value); }
            if (Company.AddDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", Company.AddDate);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", DBNull.Value); }
            if (Company.ArchiveUserID.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", Company.ArchiveUserID);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value); }
            if (Company.ArchiveDate.HasValue == true) {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", Company.ArchiveDate);
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
 
