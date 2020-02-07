using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class ReconciliationData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ReconciliationSelectAll]";
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
            string selectProcedure = "[ReconciliationSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Reconciliation I D") {
                selectCommand.Parameters.AddWithValue("@ReconciliationID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@ReconciliationID", DBNull.Value); }
            if (sField == "Invoice I D") {
                selectCommand.Parameters.AddWithValue("@InvoiceNo", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@InvoiceNo", DBNull.Value); }
            if (sField == "Payment Date") {
                selectCommand.Parameters.AddWithValue("@PaymentDate", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@PaymentDate", DBNull.Value); }
            if (sField == "Payment Amount") {
                selectCommand.Parameters.AddWithValue("@PaymentAmount", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@PaymentAmount", DBNull.Value); }
            if (sField == "T D S Amount") {
                selectCommand.Parameters.AddWithValue("@TDSAmount", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@TDSAmount", DBNull.Value); }
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

        public static Reconciliation Select_Record(Reconciliation ReconciliationPara)
        {
            Reconciliation Reconciliation = new Reconciliation();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[ReconciliationSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@ReconciliationID", ReconciliationPara.ReconciliationID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Reconciliation.ReconciliationID = System.Convert.ToInt32(reader["ReconciliationID"]);
                    Reconciliation.InvoiceID = System.Convert.ToInt32(reader["InvoiceID"]);
                    Reconciliation.PaymentDate = System.Convert.ToDateTime(reader["PaymentDate"]);
                    Reconciliation.PaymentAmount = System.Convert.ToDecimal(reader["PaymentAmount"]);
                    Reconciliation.TDSAmount = System.Convert.ToDecimal(reader["TDSAmount"]);
                    Reconciliation.Remarks = reader["Remarks"] is DBNull ? null : reader["Remarks"].ToString();
                }
                else
                {
                    Reconciliation = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Reconciliation;
            }
            finally
            {
                connection.Close();
            }
            return Reconciliation;
        }

        public static bool Add(Reconciliation Reconciliation)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[ReconciliationInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@InvoiceID", Reconciliation.InvoiceID);
            insertCommand.Parameters.AddWithValue("@PaymentDate", Reconciliation.PaymentDate);
            insertCommand.Parameters.AddWithValue("@PaymentAmount", Reconciliation.PaymentAmount);
            insertCommand.Parameters.AddWithValue("@TDSAmount", Reconciliation.TDSAmount);
            if (Reconciliation.Remarks != null) {
                insertCommand.Parameters.AddWithValue("@Remarks", Reconciliation.Remarks);
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

        public static bool Update(Reconciliation oldReconciliation, 
               Reconciliation newReconciliation)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[ReconciliationUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewInvoiceID", newReconciliation.InvoiceID);
            updateCommand.Parameters.AddWithValue("@NewPaymentDate", newReconciliation.PaymentDate);
            updateCommand.Parameters.AddWithValue("@NewPaymentAmount", newReconciliation.PaymentAmount);
            updateCommand.Parameters.AddWithValue("@NewTDSAmount", newReconciliation.TDSAmount);
            if (newReconciliation.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@NewRemarks", newReconciliation.Remarks);
            } else {
                updateCommand.Parameters.AddWithValue("@NewRemarks", DBNull.Value); }
            updateCommand.Parameters.AddWithValue("@OldReconciliationID", oldReconciliation.ReconciliationID);
            updateCommand.Parameters.AddWithValue("@OldInvoiceID", oldReconciliation.InvoiceID);
            updateCommand.Parameters.AddWithValue("@OldPaymentDate", oldReconciliation.PaymentDate);
            updateCommand.Parameters.AddWithValue("@OldPaymentAmount", oldReconciliation.PaymentAmount);
            updateCommand.Parameters.AddWithValue("@OldTDSAmount", oldReconciliation.TDSAmount);
            if (oldReconciliation.Remarks != null) {
                updateCommand.Parameters.AddWithValue("@OldRemarks", oldReconciliation.Remarks);
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

        public static bool Delete(Reconciliation Reconciliation)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[ReconciliationDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldReconciliationID", Reconciliation.ReconciliationID);
            deleteCommand.Parameters.AddWithValue("@OldInvoiceID", Reconciliation.InvoiceID);
            deleteCommand.Parameters.AddWithValue("@OldPaymentDate", Reconciliation.PaymentDate);
            deleteCommand.Parameters.AddWithValue("@OldPaymentAmount", Reconciliation.PaymentAmount);
            deleteCommand.Parameters.AddWithValue("@OldTDSAmount", Reconciliation.TDSAmount);
            if (Reconciliation.Remarks != null) {
                deleteCommand.Parameters.AddWithValue("@OldRemarks", Reconciliation.Remarks);
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
 
