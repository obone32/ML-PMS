using System;
using System.Data;
using System.Data.SqlClient;
using CloudTrixApp.Models;

namespace CloudTrixApp.Data
{
    public class EmployeeData
    {

        public static DataTable SelectAll()
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[EmployeeSelectAll]";
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
            string selectProcedure = "[EmployeeSearch]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            if (sField == "Employee I D")
            {
                selectCommand.Parameters.AddWithValue("@EmployeeID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@EmployeeID", DBNull.Value);
            }
            if (sField == "First Name")
            {
                selectCommand.Parameters.AddWithValue("@FirstName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@FirstName", DBNull.Value);
            }
            if (sField == "Last Name")
            {
                selectCommand.Parameters.AddWithValue("@LastName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@LastName", DBNull.Value);
            }
            if (sField == "D O B")
            {
                selectCommand.Parameters.AddWithValue("@DOB", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@DOB", DBNull.Value);
            }
            if (sField == "D O J")
            {
                selectCommand.Parameters.AddWithValue("@DOJ", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@DOJ", DBNull.Value);
            }
            if (sField == "Gender")
            {
                selectCommand.Parameters.AddWithValue("@Gender", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Gender", DBNull.Value);
            }
            if (sField == "E Mail")
            {
                selectCommand.Parameters.AddWithValue("@EMail", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@EMail", DBNull.Value);
            }
            if (sField == "Mobile")
            {
                selectCommand.Parameters.AddWithValue("@Mobile", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Mobile", DBNull.Value);
            }
            if (sField == "Address1")
            {
                selectCommand.Parameters.AddWithValue("@Address1", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Address1", DBNull.Value);
            }
            if (sField == "Address2")
            {
                selectCommand.Parameters.AddWithValue("@Address2", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Address2", DBNull.Value);
            }
            if (sField == "Salary")
            {
                selectCommand.Parameters.AddWithValue("@Salary", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Salary", DBNull.Value);
            }
            if (sField == "Signature U R L")
            {
                selectCommand.Parameters.AddWithValue("@SignatureURL", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@SignatureURL", DBNull.Value);
            }
            if (sField == "User Name")
            {
                selectCommand.Parameters.AddWithValue("@UserName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@UserName", DBNull.Value);
            }
            if (sField == "Password")
            {
                selectCommand.Parameters.AddWithValue("@Password", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@Password", DBNull.Value);
            }
            if (sField == "Company I D")
            {
                selectCommand.Parameters.AddWithValue("@CompanyName", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@CompanyName", DBNull.Value);
            }
            if (sField == "UserTypeID")
            {
                selectCommand.Parameters.AddWithValue("@UserTypeID", sValue);
            }
            else
            {
                selectCommand.Parameters.AddWithValue("@UserTypeID", DBNull.Value);
            }
            if (sField == "UserTypeID")
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

        public static Employee Select_Record(Employee EmployeePara)
        {
            Employee Employee = new Employee();
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[EmployeeSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@EmployeeID", EmployeePara.EmployeeID);
            try
            {
                connection.Open();
                SqlDataReader reader
                    = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    Employee.EmployeeID = System.Convert.ToInt32(reader["EmployeeID"]);
                    Employee.FirstName = System.Convert.ToString(reader["FirstName"]);
                    Employee.LastName = reader["LastName"] is DBNull ? null : reader["LastName"].ToString();
                    Employee.DOB = System.Convert.ToDateTime(reader["DOB"]);
                    Employee.DOJ = System.Convert.ToDateTime(reader["DOJ"]);
                    Employee.Gender = System.Convert.ToString(reader["Gender"]);
                    Employee.EMail = reader["EMail"] is DBNull ? null : reader["EMail"].ToString();
                    Employee.Mobile = System.Convert.ToString(reader["Mobile"]);
                    Employee.Address1 = reader["Address1"] is DBNull ? null : reader["Address1"].ToString();
                    Employee.Address2 = reader["Address2"] is DBNull ? null : reader["Address2"].ToString();
                    Employee.Salary = System.Convert.ToDecimal(reader["Salary"]);
                    Employee.SignatureURL = reader["SignatureURL"] is DBNull ? null : reader["SignatureURL"].ToString();
                    Employee.UserName = System.Convert.ToString(reader["UserName"]);
                    Employee.Password = System.Convert.ToString(reader["Password"]);
                    Employee.CompanyID = System.Convert.ToInt32(reader["CompanyID"]);
                    Employee.UserTypeID = System.Convert.ToInt32(reader["UserTypeID"]);
                    Employee.AddUserID = System.Convert.ToInt32(reader["AddUserID"]);
                    Employee.AddDate = System.Convert.ToDateTime(reader["AddDate"]);
                    Employee.IsActive = reader["IsActive"] is DBNull ? false : System.Convert.ToBoolean(reader["IsActive"]);
                    Employee.ArchiveUserID = reader["ArchiveUserID"] is DBNull ? null : (Int32?)reader["ArchiveUserID"];
                    Employee.ArchiveDate = reader["ArchiveDate"] is DBNull ? null : (DateTime?)reader["ArchiveDate"];
                }
                else
                {
                    Employee = null;
                }
                reader.Close();
            }
            catch (SqlException)
            {
                return Employee;
            }
            finally
            {
                connection.Close();
            }
            return Employee;
        }

        public static bool Add(Employee Employee)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string insertProcedure = "[EmployeeInsert]";
            SqlCommand insertCommand = new SqlCommand(insertProcedure, connection);
            insertCommand.CommandType = CommandType.StoredProcedure;
            insertCommand.Parameters.AddWithValue("@FirstName", Employee.FirstName);
            if (Employee.LastName != null)
            {
                insertCommand.Parameters.AddWithValue("@LastName", Employee.LastName);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@LastName", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@DOB", Employee.DOB);
            insertCommand.Parameters.AddWithValue("@DOJ", Employee.DOJ);
            insertCommand.Parameters.AddWithValue("@Gender", Employee.Gender);
            if (Employee.EMail != null)
            {
                insertCommand.Parameters.AddWithValue("@EMail", Employee.EMail);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@EMail", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@Mobile", Employee.Mobile);
            if (Employee.Address1 != null)
            {
                insertCommand.Parameters.AddWithValue("@Address1", Employee.Address1);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Address1", DBNull.Value);
            }
            if (Employee.Address2 != null)
            {
                insertCommand.Parameters.AddWithValue("@Address2", Employee.Address2);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@Address2", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@Salary", Employee.Salary);
            if (Employee.SignatureURL != null)
            {
                insertCommand.Parameters.AddWithValue("@SignatureURL", Employee.SignatureURL);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@SignatureURL", DBNull.Value);
            }
            insertCommand.Parameters.AddWithValue("@UserName", Employee.UserName);
            insertCommand.Parameters.AddWithValue("@Password", Employee.Password);
            insertCommand.Parameters.AddWithValue("@CompanyID", Employee.CompanyID);
            insertCommand.Parameters.AddWithValue("@AddUserID", Employee.AddUserID);
            insertCommand.Parameters.AddWithValue("@UserTypeID", Employee.UserTypeID);
            insertCommand.Parameters.AddWithValue("@AddDate", Employee.AddDate);
            insertCommand.Parameters.AddWithValue("@IsActive", Employee.IsActive);
            if (Employee.ArchiveUserID.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", Employee.ArchiveUserID);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ArchiveUserID", DBNull.Value);
            }
            if (Employee.ArchiveDate.HasValue == true)
            {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", Employee.ArchiveDate);
            }
            else
            {
                insertCommand.Parameters.AddWithValue("@ArchiveDate", DBNull.Value);
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

        public static bool Update(Employee oldEmployee,
               Employee newEmployee)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string updateProcedure = "[EmployeeUpdate]";
            SqlCommand updateCommand = new SqlCommand(updateProcedure, connection);
            updateCommand.CommandType = CommandType.StoredProcedure;
            updateCommand.Parameters.AddWithValue("@NewFirstName", newEmployee.FirstName);
            if (newEmployee.LastName != null)
            {
                updateCommand.Parameters.AddWithValue("@NewLastName", newEmployee.LastName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewLastName", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@NewDOB", newEmployee.DOB);
            updateCommand.Parameters.AddWithValue("@NewDOJ", newEmployee.DOJ);
            updateCommand.Parameters.AddWithValue("@NewGender", newEmployee.Gender);
            if (newEmployee.EMail != null)
            {
                updateCommand.Parameters.AddWithValue("@NewEMail", newEmployee.EMail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewEMail", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@NewMobile", newEmployee.Mobile);
            if (newEmployee.Address1 != null)
            {
                updateCommand.Parameters.AddWithValue("@NewAddress1", newEmployee.Address1);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewAddress1", DBNull.Value);
            }
            if (newEmployee.Address2 != null)
            {
                updateCommand.Parameters.AddWithValue("@NewAddress2", newEmployee.Address2);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewAddress2", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@NewSalary", newEmployee.Salary);
            if (newEmployee.SignatureURL != null)
            {
                updateCommand.Parameters.AddWithValue("@NewSignatureURL", newEmployee.SignatureURL);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewSignatureURL", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@NewUserName", newEmployee.UserName);
            updateCommand.Parameters.AddWithValue("@NewPassword", newEmployee.Password);
            updateCommand.Parameters.AddWithValue("@NewCompanyID", newEmployee.CompanyID);
            updateCommand.Parameters.AddWithValue("@NewUserTypeID", newEmployee.UserTypeID);
            updateCommand.Parameters.AddWithValue("@NewAddUserID", newEmployee.AddUserID);
            updateCommand.Parameters.AddWithValue("@NewAddDate", newEmployee.AddDate);
            updateCommand.Parameters.AddWithValue("@NewIsActive", newEmployee.IsActive);
            if (newEmployee.ArchiveUserID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", newEmployee.ArchiveUserID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewArchiveUserID", DBNull.Value);
            }
            if (newEmployee.ArchiveDate.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", newEmployee.ArchiveDate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@NewArchiveDate", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldEmployeeID", oldEmployee.EmployeeID);
            updateCommand.Parameters.AddWithValue("@OldFirstName", oldEmployee.FirstName);
            if (oldEmployee.LastName != null)
            {
                updateCommand.Parameters.AddWithValue("@OldLastName", oldEmployee.LastName);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldLastName", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldDOB", oldEmployee.DOB);
            updateCommand.Parameters.AddWithValue("@OldDOJ", oldEmployee.DOJ);
            updateCommand.Parameters.AddWithValue("@OldGender", oldEmployee.Gender);
            if (oldEmployee.EMail != null)
            {
                updateCommand.Parameters.AddWithValue("@OldEMail", oldEmployee.EMail);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldMobile", oldEmployee.Mobile);
            if (oldEmployee.Address1 != null)
            {
                updateCommand.Parameters.AddWithValue("@OldAddress1", oldEmployee.Address1);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value);
            }
            if (oldEmployee.Address2 != null)
            {
                updateCommand.Parameters.AddWithValue("@OldAddress2", oldEmployee.Address2);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldSalary", oldEmployee.Salary);
            if (oldEmployee.SignatureURL != null)
            {
                updateCommand.Parameters.AddWithValue("@OldSignatureURL", oldEmployee.SignatureURL);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldSignatureURL", DBNull.Value);
            }
            updateCommand.Parameters.AddWithValue("@OldUserName", oldEmployee.UserName);
            updateCommand.Parameters.AddWithValue("@OldPassword", oldEmployee.Password);
            updateCommand.Parameters.AddWithValue("@OldCompanyID", oldEmployee.CompanyID);
            updateCommand.Parameters.AddWithValue("@OldUserTypeID", oldEmployee.UserTypeID);
            updateCommand.Parameters.AddWithValue("@OldAddUserID", oldEmployee.AddUserID);
            updateCommand.Parameters.AddWithValue("@OldAddDate", oldEmployee.AddDate);
            updateCommand.Parameters.AddWithValue("@OldIsActive", oldEmployee.IsActive);
            if (oldEmployee.ArchiveUserID.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", oldEmployee.ArchiveUserID);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value);
            }
            if (oldEmployee.ArchiveDate.HasValue == true)
            {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", oldEmployee.ArchiveDate);
            }
            else
            {
                updateCommand.Parameters.AddWithValue("@OldArchiveDate", DBNull.Value);
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

        public static bool Delete(Employee Employee)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string deleteProcedure = "[EmployeeDelete]";
            SqlCommand deleteCommand = new SqlCommand(deleteProcedure, connection);
            deleteCommand.CommandType = CommandType.StoredProcedure;
            deleteCommand.Parameters.AddWithValue("@OldEmployeeID", Employee.EmployeeID);
            deleteCommand.Parameters.AddWithValue("@OldFirstName", Employee.FirstName);
            if (Employee.LastName != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldLastName", Employee.LastName);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldLastName", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldDOB", Employee.DOB);
            deleteCommand.Parameters.AddWithValue("@OldDOJ", Employee.DOJ);
            deleteCommand.Parameters.AddWithValue("@OldGender", Employee.Gender);
            if (Employee.EMail != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldEMail", Employee.EMail);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldEMail", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldMobile", Employee.Mobile);
            if (Employee.Address1 != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", Employee.Address1);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldAddress1", DBNull.Value);
            }
            if (Employee.Address2 != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", Employee.Address2);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldAddress2", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldSalary", Employee.Salary);
            if (Employee.SignatureURL != null)
            {
                deleteCommand.Parameters.AddWithValue("@OldSignatureURL", Employee.SignatureURL);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldSignatureURL", DBNull.Value);
            }
            deleteCommand.Parameters.AddWithValue("@OldUserName", Employee.UserName);
            deleteCommand.Parameters.AddWithValue("@OldPassword", Employee.Password);
            deleteCommand.Parameters.AddWithValue("@OldCompanyID", Employee.CompanyID);
            deleteCommand.Parameters.AddWithValue("@OldUserTypeID", Employee.UserTypeID);
            deleteCommand.Parameters.AddWithValue("@OldAddUserID", Employee.AddUserID);
            deleteCommand.Parameters.AddWithValue("@OldAddDate", Employee.AddDate);
            deleteCommand.Parameters.AddWithValue("@OldIsActive", Employee.IsActive);
            if (Employee.ArchiveUserID.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", Employee.ArchiveUserID);
            }
            else
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveUserID", DBNull.Value);
            }
            if (Employee.ArchiveDate.HasValue == true)
            {
                deleteCommand.Parameters.AddWithValue("@OldArchiveDate", Employee.ArchiveDate);
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

        public static DataTable SelectLoginData(string Username)
        {
            SqlConnection connection = PMMSData.GetConnection();
            string selectProcedure = "[Login_EmployeeSelect]";
            SqlCommand selectCommand = new SqlCommand(selectProcedure, connection);
            selectCommand.CommandType = CommandType.StoredProcedure;
            selectCommand.Parameters.AddWithValue("@Username", Username);
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

