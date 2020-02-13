using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Employee I D")]
        public Int32 EmployeeID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "D O B")]
        public DateTime DOB { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "D O J")]
        public DateTime DOJ { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Gender")]
        public String Gender { get; set; }

        [StringLength(8000)]
        [Display(Name = "E Mail")]
        public String EMail { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Mobile")]
        public String Mobile { get; set; }

        [StringLength(8000)]
        [Display(Name = "Address1")]
        public String Address1 { get; set; }

        [StringLength(8000)]
        [Display(Name = "Address2")]
        public String Address2 { get; set; }

        [Required]
        [Display(Name = "Salary")]
        public Decimal Salary { get; set; }

        [StringLength(8000)]
        [Display(Name = "Signature U R L")]
        public String SignatureURL { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "User Name")]
        public String UserName { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Password")]
        public String Password { get; set; }

        [Required]
        [Display(Name = "Company I D")]
        public Int32 CompanyID { get; set; }

        [Required]
        [Display(Name = "UserTypeID")]
        public Int32 UserTypeID { get; set; }

        [Required]
        [Display(Name = "Add User I D")]
        public Int32 AddUserID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Add Date")]
        public DateTime AddDate { get; set; }

        [Display(Name = "Archive User I D")]
        public Int32? ArchiveUserID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Archive Date")]
        public DateTime? ArchiveDate { get; set; }

        [Required]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
        public bool Active { get; set; }
        // ComboBox
        public virtual Company Company { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
 
