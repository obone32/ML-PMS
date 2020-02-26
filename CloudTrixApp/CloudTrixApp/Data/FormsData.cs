using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class FormsData
    {
        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[FormsSelectAll]";
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
            string selectProcedure = "[FormsSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Form I D")
            {
                selectCommand.Parameters.AddWithValue("@FormID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@FormID", DBNull.Value);
            }
            if (sField == "Form Name")
            {
                selectCommand.Parameters.AddWithValue("@FormName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@FormName", DBNull.Value);
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

        public static Forms Select_Record(Forms FormsPara)
        {
            Forms Forms = new Forms();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[FormsSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@FormID", FormsPara.FormID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Forms.FormID = System.Convert.ToInt32(reader["FormID"]);
                    Forms.FormName = System.Convert.ToString(reader["FormName"]);
                }
                else
                {
                    Forms = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Forms;
            }
            finally
            {
                connection.Close();
            }
            return Forms;
        }

        public static bool Add(Forms Forms)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[FormsInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@FormID", Forms.FormID);
            insertCommand.Parameters.AddWithValue("@FormName", Forms.FormName);
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

        public static bool Update(Forms oldForms,
               Forms newForms)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[FormsUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewFormID", newForms.FormID);
            updateCommand.Parameters.AddWithValue("@NewFormName", newForms.FormName);
            updateCommand.Parameters.AddWithValue("@OldFormID", oldForms.FormID);
            updateCommand.Parameters.AddWithValue("@OldFormName", oldForms.FormName);
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

        public static bool Delete(Forms Forms)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[FormsDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldFormID", Forms.FormID);
            deleteCommand.Parameters.AddWithValue("@OldFormName", Forms.FormName);
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
        //public static DataTable SelectAll()
        //{
        //    SqlConnection connection = PMMSData.GetConnection();
        //    string selectProcedure = "[FormSelectAll]";
        //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        //    selectCommand.CommandType = CommandType.StoredProcedure;
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = selectCommand.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            dt.Load(reader);
        //        }
        //        reader.Close();
        //    }
        //    catch (SqlException)
        //    {
        //        return dt;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return dt;
        //}
        //public static List<Form> List()
        //{
        //    List<Form> FormList = new List<Form>();
        //    SqlConnection connection = PMMSData.GetConnection();
        //    String selectProcedure = "[FormSelectAll]";
        //    SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
        //    try
        //    {
        //        connection.Open();
        //        SqlDataReader reader = selectCommand.ExecuteReader();
        //        Form Form = new Form();
        //        while (reader.Read())
        //        {
        //            Form = new Form();
        //            Form.FormID = System.Convert.ToInt32(reader["FormID"]);
        //            Form.FormName = Convert.ToString(reader["FormName"]);
        //            FormList.Add(Form);
        //        }
        //        reader.Close();
        //    }
        //    catch (SqlException)
        //    {
        //        return FormList;
        //    }
        //    finally
        //    {
        //        connection.Close();
        //    }
        //    return FormList;
        //}
    }
}