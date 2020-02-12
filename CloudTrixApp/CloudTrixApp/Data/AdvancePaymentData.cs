using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class AdvancePaymentData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[AdvancePaymentSelectAll]";
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
            string selectProcedure = "[AdvancePaymentSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Advance Payment I D") {
                selectCommand.Parameters.AddWithValue("@AdvancePaymentID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AdvancePaymentID", DBNull.Value); }
            if (sField == "Payment Date") {
                selectCommand.Parameters.AddWithValue("@PaymentDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@PaymentDate", DBNull.Value); }
            if (sField == "Company I D") {
                selectCommand.Parameters.AddWithValue("@CompanyName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CompanyName", DBNull.Value); }
            if (sField == "Client I D") {
                selectCommand.Parameters.AddWithValue("@ClientName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ClientName", DBNull.Value); }
            if (sField == "Project I D") {
                selectCommand.Parameters.AddWithValue("@ProjectName", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ProjectName", DBNull.Value); }
            if (sField == "Gross Amount") {
                selectCommand.Parameters.AddWithValue("@GrossAmount", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@GrossAmount", DBNull.Value); }
            if (sField == "T D S Rate") {
                selectCommand.Parameters.AddWithValue("@TDSRate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TDSRate", DBNull.Value); }
            if (sField == "C G S T Rate") {
                selectCommand.Parameters.AddWithValue("@CGSTRate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@CGSTRate", DBNull.Value); }
            if (sField == "S G S T Rate") {
                selectCommand.Parameters.AddWithValue("@SGSTRate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@SGSTRate", DBNull.Value); }
            if (sField == "I G S T Rate") {
                selectCommand.Parameters.AddWithValue("@IGSTRate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@IGSTRate", DBNull.Value); }
            if (sField == "Remarks") {
                selectCommand.Parameters.AddWithValue("@Remarks", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@Remarks", DBNull.Value); }
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

        public static AdvancePayment Select_Record(AdvancePayment AdvancePaymentPara)
        {
            AdvancePayment AdvancePayment = new AdvancePayment();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[AdvancePaymentSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@AdvancePaymentID", AdvancePaymentPara.AdvancePaymentID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    AdvancePayment.AdvancePaymentID = System.Convert.ToInt32(reader["AdvancePaymentID"]);
                    AdvancePayment.PaymentDate = System.Convert.ToDateTime(reader["PaymentDate"]);
                    AdvancePayment.CompanyID = System.Convert.ToInt32(reader["CompanyID"]);
                    AdvancePayment.ClientID = System.Convert.ToInt32(reader["ClientID"]);
                    AdvancePayment.ProjectID = System.Convert.ToInt32(reader["ProjectID"]);
                    AdvancePayment.GrossAmount = System.Convert.ToDecimal(reader["GrossAmount"]);
                    AdvancePayment.TDSRate = System.Convert.ToDecimal(reader["TDSRate"]);
                    AdvancePayment.CGSTRate = System.Convert.ToDecimal(reader["CGSTRate"]);
                    AdvancePayment.SGSTRate = System.Convert.ToDecimal(reader["SGSTRate"]);
                    AdvancePayment.IGSTRate = System.Convert.ToDecimal(reader["IGSTRate"]);
                    AdvancePayment.Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString();
                }
                else
                {
                    AdvancePayment = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return AdvancePayment;
            }
            finally
            {
                connection.Close();
            }
            return AdvancePayment;
        }

        public static bool Add(AdvancePayment AdvancePayment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[AdvancePaymentInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@PaymentDate", AdvancePayment.PaymentDate);
            insertCommand.Parameters.AddWithValue("@CompanyID", AdvancePayment.CompanyID);
            insertCommand.Parameters.AddWithValue("@ClientID", AdvancePayment.ClientID);
            insertCommand.Parameters.AddWithValue("@ProjectID", AdvancePayment.ProjectID);
            insertCommand.Parameters.AddWithValue("@GrossAmount", AdvancePayment.GrossAmount);
            insertCommand.Parameters.AddWithValue("@TDSRate", AdvancePayment.TDSRate);
            insertCommand.Parameters.AddWithValue("@CGSTRate", AdvancePayment.CGSTRate);
            insertCommand.Parameters.AddWithValue("@SGSTRate", AdvancePayment.SGSTRate);
            insertCommand.Parameters.AddWithValue("@IGSTRate", AdvancePayment.IGSTRate);
            if (AdvancePayment.Remarks != null) {
                insertCommand.Parameters.AddWithValue("@Remarks", AdvancePayment.Remarks);
            } else {
                insertCommand.Parameters.AddWithValue("@Remarks", DBNull.Value); }
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

        public static bool Update(AdvancePayment oldAdvancePayment, 
               AdvancePayment newAdvancePayment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[AdvancePaymentUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewPaymentDate", newAdvancePayment.PaymentDate);
            updateCommand.Parameters.AddWithValue("@NewCompanyID", newAdvancePayment.CompanyID);
            updateCommand.Parameters.AddWithValue("@NewClientID", newAdvancePayment.ClientID);
            updateCommand.Parameters.AddWithValue("@NewProjectID", newAdvancePayment.ProjectID);
            updateCommand.Parameters.AddWithValue("@NewGrossAmount", newAdvancePayment.GrossAmount);
            updateCommand.Parameters.AddWithValue("@NewTDSRate", newAdvancePayment.TDSRate);
            updateCommand.Parameters.AddWithValue("@NewCGSTRate", newAdvancePayment.CGSTRate);
            updateCommand.Parameters.AddWithValue("@NewSGSTRate", newAdvancePayment.SGSTRate);
            updateCommand.Parameters.AddWithValue("@NewIGSTRate", newAdvancePayment.IGSTRate);
            if (newAdvancePayment.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@NewRemarks", newAdvancePayment.Remarks);
            } else {
                updateCommand.Parameters.AddWithValue("@NewRemarks", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldAdvancePaymentID", oldAdvancePayment.AdvancePaymentID);
            updateCommand.Parameters.AddWithValue("@OldPaymentDate", oldAdvancePayment.PaymentDate);
            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldAdvancePayment.CompanyID);
            updateCommand.Parameters.AddWithValue("@OldClientID", oldAdvancePayment.ClientID);
            updateCommand.Parameters.AddWithValue("@OldProjectID", oldAdvancePayment.ProjectID);
            updateCommand.Parameters.AddWithValue("@OldGrossAmount", oldAdvancePayment.GrossAmount);
            updateCommand.Parameters.AddWithValue("@OldTDSRate", oldAdvancePayment.TDSRate);
            updateCommand.Parameters.AddWithValue("@OldCGSTRate", oldAdvancePayment.CGSTRate);
            updateCommand.Parameters.AddWithValue("@OldSGSTRate", oldAdvancePayment.SGSTRate);
            updateCommand.Parameters.AddWithValue("@OldIGSTRate", oldAdvancePayment.IGSTRate);
            if (oldAdvancePayment.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@OldRemarks", oldAdvancePayment.Remarks);
            } else {
                updateCommand.Parameters.AddWithValue("@OldRemarks", DBNull.Value); }
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

        public static bool Delete(AdvancePayment AdvancePayment)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[AdvancePaymentDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldAdvancePaymentID", AdvancePayment.AdvancePaymentID);
            deleteCommand.Parameters.AddWithValue("@OldPaymentDate", AdvancePayment.PaymentDate);
            deleteCommand.Parameters.AddWithValue("@OldCompanyID", AdvancePayment.CompanyID);
            deleteCommand.Parameters.AddWithValue("@OldClientID", AdvancePayment.ClientID);
            deleteCommand.Parameters.AddWithValue("@OldProjectID", AdvancePayment.ProjectID);
            deleteCommand.Parameters.AddWithValue("@OldGrossAmount", AdvancePayment.GrossAmount);
            deleteCommand.Parameters.AddWithValue("@OldTDSRate", AdvancePayment.TDSRate);
            deleteCommand.Parameters.AddWithValue("@OldCGSTRate", AdvancePayment.CGSTRate);
            deleteCommand.Parameters.AddWithValue("@OldSGSTRate", AdvancePayment.SGSTRate);
            deleteCommand.Parameters.AddWithValue("@OldIGSTRate", AdvancePayment.IGSTRate);
            if (AdvancePayment.Remarks != null) {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", AdvancePayment.Remarks);
            } else {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", DBNull.Value); }
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
 
