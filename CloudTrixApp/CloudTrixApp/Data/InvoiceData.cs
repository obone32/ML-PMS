using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class InvoiceData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceSelectAll]";
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
            string selectProcedure = "[InvoiceSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Invoice I D")
            {
                selectCommand.Parameters.AddWithValue("@InvoiceID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@InvoiceID", DBNull.Value);
            }
            if (sField == "Invoice No")
            {
                selectCommand.Parameters.AddWithValue("@InvoiceNo", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@InvoiceNo", DBNull.Value);
            }
            if (sField == "Invoice Date")
            {
                selectCommand.Parameters.AddWithValue("@InvoiceDate", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@InvoiceDate", DBNull.Value);
            }
            if (sField == "Project I D")
            {
                selectCommand.Parameters.AddWithValue("@ProjectName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ProjectName", DBNull.Value);
            }
            if (sField == "Client I D")
            {
                selectCommand.Parameters.AddWithValue("@ClientName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ClientName", DBNull.Value);
            }
            if (sField == "Client Name")
            {
                selectCommand.Parameters.AddWithValue("@ClientName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ClientName", DBNull.Value);
            }
            if (sField == "Client Address")
            {
                selectCommand.Parameters.AddWithValue("@ClientAddress", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ClientAddress", DBNull.Value);
            }
            if (sField == "Client G S T I N")
            {
                selectCommand.Parameters.AddWithValue("@ClientGSTIN", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ClientGSTIN", DBNull.Value);
            }
            if (sField == "Client Contact No")
            {
                selectCommand.Parameters.AddWithValue("@ClientContactNo", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ClientContactNo", DBNull.Value);
            }
            if (sField == "Client E Mail")
            {
                selectCommand.Parameters.AddWithValue("@ClientEMail", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@ClientEMail", DBNull.Value);
            }
            if (sField == "Additional Discount")
            {
                selectCommand.Parameters.AddWithValue("@AdditionalDiscount", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@AdditionalDiscount", DBNull.Value);
            }
            if (sField == "Remarks")
            {
                selectCommand.Parameters.AddWithValue("@Remarks", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Remarks", DBNull.Value);
            }
            if (sField == "P D F Url")
            {
                selectCommand.Parameters.AddWithValue("@PDFUrl", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@PDFUrl", DBNull.Value);
            }
            if (sField == "Company I D")
            {
                selectCommand.Parameters.AddWithValue("@CompanyName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@CompanyName", DBNull.Value);
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

        public static Invoice Select_Record(Invoice InvoicePara)
        {
            Invoice Invoice = new Invoice();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@InvoiceID", InvoicePara.InvoiceID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Invoice.InvoiceID = System.Convert.ToInt32(reader["InvoiceID"]);
                    Invoice.InvoiceNo = System.Convert.ToString(reader["InvoiceNo"]);
                    Invoice.InvoiceDate = System.Convert.ToDateTime(reader["InvoiceDate"]);
                    Invoice.ProjectID = reader["ProjectID"] is DBNull ? null : (Int32?)reader["ProjectID"];
                    Invoice.ClientID = System.Convert.ToInt32(reader["ClientID"]);
                    Invoice.ClientName = reader["ClientName"] is DBNull ? null : reader["ClientName"].ToString();
                    Invoice.ClientAddress = reader["ClientAddress"] is DBNull ? null : reader["ClientAddress"].ToString();
                    Invoice.ClientGSTIN = reader["ClientGSTIN"] is DBNull ? null : reader["ClientGSTIN"].ToString();
                    Invoice.ClientContactNo = reader["ClientContactNo"] is DBNull ? null : reader["ClientContactNo"].ToString();
                    Invoice.ClientEMail = reader["ClientEMail"] is DBNull ? null : reader["ClientEMail"].ToString();
                    Invoice.Discount = reader["Discount"] is DBNull ? null : (Decimal?)reader["Discount"];
                    Invoice.Notes = reader["Notes"] is DBNull ? null : reader["Notes"].ToString();
                    Invoice.CompanyID = reader["CompanyID"] is DBNull ? null : (Int32?)reader["CompanyID"];
                    Invoice.AddUserID = System.Convert.ToInt32(reader["AddUserID"]);
                    Invoice.AddDate = reader["AddDate"] is DBNull ? null : (DateTime?)reader["AddDate"];
                    Invoice.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    Invoice.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                    Invoice.GrandTotal = System.Convert.ToDecimal(reader["GrandTotal"]);
                    Invoice.Invoice_Type = reader["Invoice_Type"] is DBNull ? null : (string)(reader["Invoice_Type"]);
                    Invoice.Payment_Method = reader["Payment_Method"] is DBNull ? null : (string)(reader["Payment_Method"]);
                    Invoice.InvoiceStatus = reader["InvoiceStatus"] is DBNull ? null : (string)(reader["InvoiceStatus"]);
                }
                else
                {
                    Invoice = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Invoice;
            }
            finally
            {
                connection.Close();
            }
            return Invoice;
        }

        public static int Add(Invoice Invoice)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[InvoiceInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@InvoiceNo", Invoice.InvoiceNo);
            insertCommand.Parameters.AddWithValue("@InvoiceDate", Invoice.InvoiceDate);
            if (Invoice.ProjectID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ProjectID", Invoice.ProjectID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ProjectID", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@ClientID", Invoice.ClientID);
            if (Invoice.ClientName != null)
            {
                insertCommand.Parameters.AddWithValue("@ClientName", Invoice.ClientName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ClientName", DBNull.Value);
            }
            if (Invoice.ClientAddress != null)
            {
                insertCommand.Parameters.AddWithValue("@ClientAddress", Invoice.ClientAddress);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ClientAddress", DBNull.Value);
            }
            if (Invoice.ClientGSTIN != null)
            {
                insertCommand.Parameters.AddWithValue("@ClientGSTIN", Invoice.ClientGSTIN);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ClientGSTIN", DBNull.Value);
            }
            if (Invoice.ClientContactNo != null)
            {
                insertCommand.Parameters.AddWithValue("@ClientContactNo", Invoice.ClientContactNo);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ClientContactNo", DBNull.Value);
            }
            if (Invoice.ClientEMail != null)
            {
                insertCommand.Parameters.AddWithValue("@ClientEMail", Invoice.ClientEMail);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ClientEMail", DBNull.Value);
            }
            if (Invoice.Discount != null)
            {
                insertCommand.Parameters.AddWithValue("@Discount", Invoice.Discount);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Discount", DBNull.Value);
            }
            if (Invoice.Notes != null)
            {
                insertCommand.Parameters.AddWithValue("@Notes", Invoice.Notes);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Notes", DBNull.Value);
            }

            if (Invoice.CompanyID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@CompanyID", Invoice.CompanyID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@CompanyID", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@AddUserID", Invoice.AddUserID);
            if (Invoice.AddDate.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@AddDate", Invoice.AddDate);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@AddDate", DBNull.Value);
            }
            if (Invoice.ArchiveUserID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", Invoice.ArchiveUserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value);
            }
            if (Invoice.ArchiveDate.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", Invoice.ArchiveDate);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", DBNull.Value);
            }
            if (Invoice.GrandTotal != null)
            {
                insertCommand.Parameters.AddWithValue("@GrandTotal", Invoice.GrandTotal);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@GrandTotal", DBNull.Value);
            }

            if (Invoice.Invoice_Type != null)
            {
                insertCommand.Parameters.AddWithValue("@Invoice_Type", Invoice.Invoice_Type);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Invoice_Type", DBNull.Value);
            }

            if (Invoice.Payment_Method != null)
            {
                insertCommand.Parameters.AddWithValue("@Payment_Method", Invoice.Payment_Method);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Payment_Method", DBNull.Value);
            }

            if (Invoice.InvoiceStatus != null)
            {
                insertCommand.Parameters.AddWithValue("@InvoiceStatus", Invoice.InvoiceStatus);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@InvoiceStatus", DBNull.Value);
            }
            insertCommand.Parameters.Add("@pInvoiceID", System.Data.SqlDbType.Int);
            insertCommand.Parameters["@pInvoiceID"].Direction = ParameterDirection.Output;
            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                int count = System.Convert.ToInt32(insertCommand.Parameters["@pInvoiceID"].Value);
                if (count > 0)
                {
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(Invoice Invoice)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[InvoiceUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@InvoiceID", Invoice.InvoiceID);
            updateCommand.Parameters.AddWithValue("@InvoiceNo", Invoice.InvoiceNo);
            updateCommand.Parameters.AddWithValue("@InvoiceDate", Invoice.InvoiceDate);
            if (Invoice.ProjectID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@ProjectID", Invoice.ProjectID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ProjectID", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@ClientID", Invoice.ClientID);
            if (Invoice.ClientName != null)
            {
                updateCommand.Parameters.AddWithValue("@ClientName", Invoice.ClientName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ClientName", DBNull.Value);
            }
            if (Invoice.ClientAddress != null)
            {
                updateCommand.Parameters.AddWithValue("@ClientAddress", Invoice.ClientAddress);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ClientAddress", DBNull.Value);
            }
            if (Invoice.ClientGSTIN != null)
            {
                updateCommand.Parameters.AddWithValue("@ClientGSTIN", Invoice.ClientGSTIN);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ClientGSTIN", DBNull.Value);
            }
            if (Invoice.ClientContactNo != null)
            {
                updateCommand.Parameters.AddWithValue("@ClientContactNo", Invoice.ClientContactNo);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ClientContactNo", DBNull.Value);
            }
            if (Invoice.ClientEMail != null)
            {
                updateCommand.Parameters.AddWithValue("@ClientEMail", Invoice.ClientEMail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ClientEMail", DBNull.Value);
            }
            if (Invoice.Discount.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@Discount", Invoice.Discount);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Discount", DBNull.Value);
            }
            if (Invoice.Notes != null)
            {
                updateCommand.Parameters.AddWithValue("@Notes", Invoice.Notes);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Notes", DBNull.Value);
            }
          
            if (Invoice.CompanyID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@CompanyID", Invoice.CompanyID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@CompanyID", DBNull.Value);
            }

            updateCommand.Parameters.AddWithValue("@AddUserID", Invoice.AddUserID);
            if (Invoice.AddDate.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@AddDate", Invoice.AddDate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@AddDate", DBNull.Value);
            }
            if (Invoice.ArchiveUserID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@ArchiveUserID", Invoice.ArchiveUserID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value);
            }
            if (Invoice.ArchiveDate.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@ArchiveDate", Invoice.ArchiveDate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@ArchiveDate", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@GrandTotal", Invoice.GrandTotal);
            updateCommand.Parameters.AddWithValue("@Invoice_Type", Invoice.Invoice_Type);
            updateCommand.Parameters.AddWithValue("@Payment_Method", Invoice.Payment_Method);
            updateCommand.Parameters.AddWithValue("@InvoiceStatus", Invoice.InvoiceStatus);
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
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Delete(Invoice Invoice)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[InvoiceDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldInvoiceID", Invoice.InvoiceID);
            deleteCommand.Parameters.AddWithValue("@OldInvoiceNo", Invoice.InvoiceNo);
            deleteCommand.Parameters.AddWithValue("@OldInvoiceDate", Invoice.InvoiceDate);
            if (Invoice.ProjectID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldProjectID", Invoice.ProjectID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldProjectID", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldClientID", Invoice.ClientID);
            if (Invoice.ClientName != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldClientName", Invoice.ClientName);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldClientName", DBNull.Value);
            }
            if (Invoice.ClientAddress != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldClientAddress", Invoice.ClientAddress);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldClientAddress", DBNull.Value);
            }
            if (Invoice.ClientGSTIN != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldClientGSTIN", Invoice.ClientGSTIN);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldClientGSTIN", DBNull.Value);
            }
            if (Invoice.ClientContactNo != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldClientContactNo", Invoice.ClientContactNo);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldClientContactNo", DBNull.Value);
            }
            if (Invoice.ClientEMail != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldClientEMail", Invoice.ClientEMail);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldClientEMail", DBNull.Value);
            }
            if (Invoice.Discount.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldAdditionalDiscount", Invoice.Discount);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldAdditionalDiscount", DBNull.Value);
            }
            if (Invoice.Notes != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", Invoice.Notes);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", DBNull.Value);
            }
            if (Invoice.PDFUrl != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldPDFUrl", Invoice.PDFUrl);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldPDFUrl", DBNull.Value);
            }
            if (Invoice.CompanyID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldCompanyID", Invoice.CompanyID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldCompanyID", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldAddUserID", Invoice.AddUserID);
            if (Invoice.AddDate.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", Invoice.AddDate);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldAddDate", DBNull.Value);
            }
            if (Invoice.ArchiveUserID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", Invoice.ArchiveUserID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value);
            }
            if (Invoice.ArchiveDate.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", Invoice.ArchiveDate);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", DBNull.Value);
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
        public static Client Client_StateVerify(Client ClientPara)
        {
            Client Client = new Client();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Client_StateVerify]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@pClientID", ClientPara.ClientID);
            selectCommand.Parameters.AddWithValue("@pCompanyID", ClientPara.CompanyID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    //  Client.IsStateMatch = System.Convert.ToBoolean(reader["IsStateMatch"]);
                    Client.Address1 = Convert.ToString(reader["Address1"]);
                    Client.GSTIN = Convert.ToString(reader["GSTIN"]);
                    Client.EMail = Convert.ToString(reader["EMail"]);
                    Client.ContactNo = Convert.ToString(reader["ContactNo"]);
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

        public static Company Company_InvoiceInitials(Company CompanyPara)
        {
            Company Company = new Company();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Company_InvoiceInitials]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@CompanyID", CompanyPara.CompanyID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Company.InvoiceInitials = Convert.ToString(reader["InvoiceInitials"]);
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
        public static DataTable CGT_Apply(int CompanyID, int ClientID)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Invoice_GST_Location]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@CompanyID", CompanyID);
            selectCommand.Parameters.AddWithValue("@ClientID", ClientID);
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
    }
}

