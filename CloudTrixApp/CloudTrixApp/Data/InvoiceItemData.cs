using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class InvoiceItemData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceItemSelectAll]";
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
            string selectProcedure = "[InvoiceItemSearch]";
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
            if (sField == "Invoice Item I D")
            {
                selectCommand.Parameters.AddWithValue("@InvoiceItemID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@InvoiceItemID", DBNull.Value);
            }
            if (sField == "Description")
            {
                selectCommand.Parameters.AddWithValue("@Description", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Description", DBNull.Value);
            }
            if (sField == "Quantity")
            {
                selectCommand.Parameters.AddWithValue("@Quantity", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Quantity", DBNull.Value);
            }
            if (sField == "Rate")
            {
                selectCommand.Parameters.AddWithValue("@Rate", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Rate", DBNull.Value);
            }
            if (sField == "Discount Amount")
            {
                selectCommand.Parameters.AddWithValue("@DiscountAmount", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@DiscountAmount", DBNull.Value);
            }
            if (sField == "C G S T Rate")
            {
                selectCommand.Parameters.AddWithValue("@CGSTRate", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@CGSTRate", DBNull.Value);
            }
            if (sField == "S G S T Rate")
            {
                selectCommand.Parameters.AddWithValue("@SGSTRate", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@SGSTRate", DBNull.Value);
            }
            if (sField == "I G S T Rate")
            {
                selectCommand.Parameters.AddWithValue("@IGSTRate", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@IGSTRate", DBNull.Value);
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

        public static InvoiceItem Select_Record(InvoiceItem InvoiceItemPara)
        {
            InvoiceItem InvoiceItem = new InvoiceItem();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceItemSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@InvoiceID", InvoiceItemPara.InvoiceID);
            selectCommand.Parameters.AddWithValue("@InvoiceItemID", InvoiceItemPara.InvoiceItemID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    InvoiceItem.InvoiceID = System.Convert.ToInt32(reader["InvoiceID"]);
                    InvoiceItem.InvoiceItemID = System.Convert.ToInt32(reader["InvoiceItemID"]);
                    InvoiceItem.Description = reader["Description"] is DBNull ? null : reader["Description"].ToString();
                    InvoiceItem.Quantity = System.Convert.ToDecimal(reader["Quantity"]);
                    InvoiceItem.Rate = System.Convert.ToDecimal(reader["Rate"]);
                    InvoiceItem.CGSTRate = System.Convert.ToDecimal(reader["CGSTRate"]);
                    InvoiceItem.SGSTRate = System.Convert.ToDecimal(reader["SGSTRate"]);
                    InvoiceItem.IGSTRate = System.Convert.ToDecimal(reader["IGSTRate"]);
                }
                else
                {
                    InvoiceItem = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return InvoiceItem;
            }
            finally
            {
                connection.Close();
            }
            return InvoiceItem;
        }

        public static List<InvoiceItem> List(InvoiceItem InvoiceItemPara)
        {
            List<InvoiceItem> InvoiceItemList = new List<InvoiceItem>();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[InvoiceItemSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@InvoiceID", InvoiceItemPara.InvoiceID);
            selectCommand.Parameters.AddWithValue("@InvoiceItemID", InvoiceItemPara.InvoiceItemID);
            try
            {
                connection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();
                InvoiceItem InvoiceItem = new InvoiceItem();
                while (reader.Read())
                {
                    InvoiceItem = new InvoiceItem();
                    InvoiceItem.InvoiceID = System.Convert.ToInt32(reader["InvoiceID"]);
                    InvoiceItem.InvoiceItemID = System.Convert.ToInt32(reader["InvoiceItemID"]);
                    InvoiceItem.Description = reader["Description"] is DBNull ? null : reader["Description"].ToString();
                    InvoiceItem.Quantity = System.Convert.ToDecimal(reader["Quantity"]);
                    InvoiceItem.Rate = System.Convert.ToDecimal(reader["Rate"]);
                    InvoiceItem.Tax = reader["Tax"] is DBNull ? null : reader["Tax"].ToString();
                    InvoiceItem.Amount = System.Convert.ToDecimal(reader["Amount"]);
                    InvoiceItem.CGSTRate = System.Convert.ToDecimal(reader["CGSTRate"]);
                    InvoiceItem.CGST_Amt = System.Convert.ToDecimal(reader["CGST_Amt"]);
                    InvoiceItem.SGSTRate = System.Convert.ToDecimal(reader["SGSTRate"]);
                    InvoiceItem.SGST_Amt = System.Convert.ToDecimal(reader["SGST_Amt"]);
                    InvoiceItem.IGSTRate = System.Convert.ToDecimal(reader["IGSTRate"]);
                    InvoiceItem.IGST_Amt = System.Convert.ToDecimal(reader["IGST_Amt"]);
                    InvoiceItem.Total_Amt = System.Convert.ToDecimal(reader["Total_Amt"]);
                    InvoiceItemList.Add(InvoiceItem);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                return InvoiceItemList;
            }
            finally
            {
                connection.Close();
            }
            return InvoiceItemList;
        }

        public static bool Add(InvoiceItem InvoiceItem)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[InvoiceItemInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@InvoiceID", InvoiceItem.InvoiceID);
            insertCommand.Parameters.AddWithValue("@InvoiceItemID", InvoiceItem.InvoiceItemID);
            if (InvoiceItem.Description != null)
            {
                insertCommand.Parameters.AddWithValue("@Description", InvoiceItem.Description);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Description", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@Quantity", InvoiceItem.Quantity);
            insertCommand.Parameters.AddWithValue("@Rate", InvoiceItem.Rate);
            insertCommand.Parameters.AddWithValue("@CGSTRate", InvoiceItem.CGSTRate);
            insertCommand.Parameters.AddWithValue("@SGSTRate", InvoiceItem.SGSTRate);
            insertCommand.Parameters.AddWithValue("@IGSTRate", InvoiceItem.IGSTRate);
            insertCommand.Parameters.AddWithValue("@Tax", InvoiceItem.Tax);
            insertCommand.Parameters.AddWithValue("@Amount ", InvoiceItem.Amount);
            insertCommand.Parameters.AddWithValue("@IGST_Amt", InvoiceItem.IGST_Amt);
            insertCommand.Parameters.AddWithValue("@CGST_Amt", InvoiceItem.CGST_Amt);
            insertCommand.Parameters.AddWithValue("@SGST_Amt", InvoiceItem.SGST_Amt);
            insertCommand.Parameters.AddWithValue("@Total_Amt", InvoiceItem.Total_Amt);
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
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool Update(InvoiceItem InvoiceItem)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[InvoiceItemUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@InvoiceID", InvoiceItem.InvoiceID);
            updateCommand.Parameters.AddWithValue("@InvoiceItemID", InvoiceItem.InvoiceItemID);
            if (InvoiceItem.Description != null)
            {
                updateCommand.Parameters.AddWithValue("@Description", InvoiceItem.Description);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@Description", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@Quantity", InvoiceItem.Quantity);
            updateCommand.Parameters.AddWithValue("@Rate", InvoiceItem.Rate);
            updateCommand.Parameters.AddWithValue("@CGSTRate", InvoiceItem.CGSTRate);
            updateCommand.Parameters.AddWithValue("@SGSTRate", InvoiceItem.SGSTRate);
            updateCommand.Parameters.AddWithValue("@IGSTRate", InvoiceItem.IGSTRate);
            updateCommand.Parameters.AddWithValue("@Tax", InvoiceItem.Tax);
            updateCommand.Parameters.AddWithValue("@Amount ", InvoiceItem.Amount);
            updateCommand.Parameters.AddWithValue("@IGST_Amt", InvoiceItem.IGST_Amt);
            updateCommand.Parameters.AddWithValue("@CGST_Amt", InvoiceItem.CGST_Amt);
            updateCommand.Parameters.AddWithValue("@SGST_Amt", InvoiceItem.SGST_Amt);
            updateCommand.Parameters.AddWithValue("@Total_Amt", InvoiceItem.Total_Amt);
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

        public static bool Delete(InvoiceItem InvoiceItem)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[InvoiceItemDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldInvoiceItemID", InvoiceItem.InvoiceItemID);

            try
            {
                connection.Open();
                deleteCommand.ExecuteNonQuery();
                //int count = System.Convert.ToInt32(deleteCommand.Parameters["@ReturnValue"].Value);
                //if (count > 0)
                //{
                return true;
                //}
                //else
                //{
                //    return false;
                //}
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
    }
}

