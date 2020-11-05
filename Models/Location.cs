namespace TTG3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location")]
    public partial class Location
    {
        [Key]
        public int Location_ID { get; set; }

        [Required]
        [Display(Name = "Room")]
        [StringLength(20)]
        [MinLength(6,ErrorMessage = "Minimum 6 characters are Required"),MaxLength(20,ErrorMessage = "Room Location cannot be greater than 20 characters")]
        public string Room { get; set; }

        [Required]
        [Display(Name = "Semester Number")]
        [Range(1, 5, ErrorMessage = "Semester Number should be between 1 and 5")]
        public int Semester_Number { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Subject Type")]
        public string Subject_Type { get; set; }
    }
}
