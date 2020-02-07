using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class InvoiceAdvanceData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceAdvanceSelectAll]";
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
            string selectProcedure = "[InvoiceAdvanceSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Invoice I D") {
                selectCommand.Parameters.AddWithValue("@InvoiceID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@InvoiceID", DBNull.Value); }
            if (sField == "Advance Payment I D") {
                selectCommand.Parameters.AddWithValue("@AdvancePaymentID", sValue);
            } else {
                selectCommand.Parameters.AddWithValue("@AdvancePaymentID", DBNull.Value); }
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

        public static InvoiceAdvance Select_Record(InvoiceAdvance InvoiceAdvancePara)
        {
            InvoiceAdvance InvoiceAdvance = new InvoiceAdvance();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceAdvanceSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@InvoiceID", InvoiceAdvancePara.InvoiceID);
            selectCommand.Parameters.AddWithValue("@AdvancePaymentID", InvoiceAdvancePara.AdvancePaymentID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    InvoiceAdvance.InvoiceID = System.Convert.ToInt32(reader["InvoiceID"]);
                    InvoiceAdvance.AdvancePaymentID = System.Convert.ToInt32(reader["AdvancePaymentID"]);
                }
                else
                {
                    InvoiceAdvance = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return InvoiceAdvance;
            }
            finally
            {
                connection.Close();
            }
            return InvoiceAdvance;
        }

        public static bool Add(InvoiceAdvance InvoiceAdvance)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[InvoiceAdvanceInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@InvoiceID", InvoiceAdvance.InvoiceID);
            insertCommand.Parameters.AddWithValue("@AdvancePaymentID", InvoiceAdvance.AdvancePaymentID);
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

        public static bool Update(InvoiceAdvance oldInvoiceAdvance, 
               InvoiceAdvance newInvoiceAdvance)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[InvoiceAdvanceUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewInvoiceID", newInvoiceAdvance.InvoiceID);
            updateCommand.Parameters.AddWithValue("@NewAdvancePaymentID", newInvoiceAdvance.AdvancePaymentID);
            updateCommand.Parameters.AddWithValue("@OldInvoiceID", oldInvoiceAdvance.InvoiceID);
            updateCommand.Parameters.AddWithValue("@OldAdvancePaymentID", oldInvoiceAdvance.AdvancePaymentID);
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

        public static bool Delete(InvoiceAdvance InvoiceAdvance)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[InvoiceAdvanceDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldInvoiceID", InvoiceAdvance.InvoiceID);
            deleteCommand.Parameters.AddWithValue("@OldAdvancePaymentID", InvoiceAdvance.AdvancePaymentID);
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
 
