namespace TTG3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Session")]
    public partial class Session
    {
        [Key]
        public int Session_ID { get; set; }

        [Required(ErrorMessage = "Semester Number is Required")]
        [Display(Name = "Semester Number")]
        [Range(1, 5, ErrorMessage = "Semester Number should be between 1 and 5")]
        public int Semester_Number { get; set; }

        [Required(ErrorMessage = "Subject is Required")]
        [Display(Name = "Subject")]
        public int Subject_ID { get; set; }

        [Required(ErrorMessage = "Professor is Required")]
        [Display(Name = "Professor")]
        public int Professor_ID { get; set; }

        [Required(ErrorMessage = "Subject Type is Required")]
        [StringLength(20)]
        [Display(Name = "Subject Type")]
        public string Subject_Type { get; set; }

        [Required(ErrorMessage = "Total Count is Required")]
        [Display(Name = "Sessions Per Week")]
        [Range(1,5,ErrorMessage = "Total Count should be between 1 and 5")]
        public int Sessions_Per_Week { get; set; }

        [Required(ErrorMessage = "Priority Level is Required")]
        [Display(Name = "Priority Level")]
        public int Priority_Level { get; set; }

        public virtual Professor Professor { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
