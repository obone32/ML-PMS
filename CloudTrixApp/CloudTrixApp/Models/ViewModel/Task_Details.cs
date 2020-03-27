using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudTrixApp.Models.ViewModel
{
    public class Task
    {
        public Int32 TaskID { get; set; }
        public String TaskName { get; set; }
        public String Description { get; set; }
        public DateTime CreationDate { get; set; }
    }
    public class TaskAssignment
    {
        public Int32 TaskAssignmentID { get; set; }
        public DateTime AssignmentDate { get; set; }
        public Int32 TaskID { get; set; }
        public Int32 EmployeeID { get; set; }
        public Int32 TaskStateID { get; set; }
        // ComboBox
        public virtual Task Task { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual TaskState TaskState { get; set; }
    }
    public class Employee
    {
        public Int32 EmployeeID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOJ { get; set; }
        public String Gender { get; set; }
        public String EMail { get; set; }
        public String Mobile { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public Decimal Salary { get; set; }
        public String SignatureURL { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public Int32 CompanyID { get; set; }
        public Int32 UserTypeID { get; set; }
        public Int32 AddUserID { get; set; }
        public DateTime AddDate { get; set; }
        public Int32? ArchiveUserID { get; set; }
        public DateTime? ArchiveDate { get; set; }
        public bool IsActive { get; set; }
        public bool Active { get; set; }
        // ComboBox
        public virtual Company Company { get; set; }
        public virtual UserType UserType { get; set; }
    }
    public class TaskState
    {
        public Int32 TaskStateID { get; set; }
        public String TaskStateName { get; set; }
    }
    public class Task_Details
    {
        public List<Task> clasTask { get; set; }
        public List<TaskState> clasTaskState { get; set; }
        public List<TaskAssignment> clsTaskAssignment { get; set; }
    }

}