using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CloudTrixApp.Models
{
    [Table("ArchitectAssociate")]
    public class ArchitectAssociate
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        [Display(Name = "Architect Associate I D")]
        public Int32 ArchitectAssociateID { get; set; }

        [Required]
        [Display(Name = "Architect I D")]
        public Int32 ArchitectID { get; set; }

        [Required]
        [StringLength(8000)]
        [Display(Name = "Associate Name")]
        public String AssociateName { get; set; }

        [StringLength(8000)]
        [Display(Name = "Contact No")]
        public String ContactNo { get; set; }

        [StringLength(8000)]
        [Display(Name = "E Mail")]
        public String EMail { get; set; }

        // ComboBox
        public virtual Architect Architect { get; set; }

    }
}

