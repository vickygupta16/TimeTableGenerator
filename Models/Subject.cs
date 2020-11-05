namespace TTG3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Subject")]
    public partial class Subject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Subject()
        {
            Sessions = new HashSet<Session>();
        }

        [Key]
        public int Subject_ID { get; set; }

        [Required(ErrorMessage = "Semester Number is Required")]
        [Display(Name = "Semester Number")]
        [Range(1,5,ErrorMessage = "Semester Number should be between 1 and 5")]
        public int Semester_Number { get; set; }

        [Required(ErrorMessage = "Subject Code is Required")]
        [Display(Name = "Subject Code")]
        [StringLength(20,ErrorMessage = "Subject Code cannot be greater than 20 characters")]
        public string Subject_Code { get; set; }

        [Required(ErrorMessage = "Subject Name is Required")]
        [Display(Name = "Subject Name")]
        [StringLength(70,ErrorMessage = "Subject Name cannot be greater than 70 characters")]
        public string Subject_Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
