namespace TTG3.Models
{
    using Foolproof;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Professor")]
    public partial class Professor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Professor()
        {
            Sessions = new HashSet<Session>();
        }

        [Key]
        public int Professor_ID { get; set; }

        [Required(ErrorMessage = "Professor Name is Required")]
        [Display(Name = "Professor Name")]
        [StringLength(50)]
        [MinLength(10,ErrorMessage = "Minimum 10 characters are required"),MaxLength(50,ErrorMessage = "Name cannot be greater than 50 characters")]
        public string Professor_Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email ID")]
        [MinLength(15,ErrorMessage = "Minimum 15 characters are required"),MaxLength(50,ErrorMessage = "Email ID cannot be greater than 50 characters")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email ID")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Contact Number is Required")]
        [Display(Name = "Contact")]
        [MinLength(10,ErrorMessage = "Contact Number should be 10 digits"),MaxLength(10,ErrorMessage = "Contact Number cannot be greater than 10 digits")]
        [StringLength(10)]
        //[RegularExpression("([1-9][0-9]*)",ErrorMessage = "Only Digits are allowed")]
        public string Contact { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        [StringLength(6)]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [Display(Name = "Age")]
        [Range(25,80,ErrorMessage = "Age should be between 25 and 80")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Option is Required")]
        [StringLength(3)]
        [Display(Name = "Visiting Faculty")]
        public string Visiting_Faculty { get; set; }

        [Required(ErrorMessage = "Start Time is Required")]
        [Display(Name = "Available From")]
        public int Start_Hour { get; set; }

        [Required(ErrorMessage = "End Time is Required")]
        [Display(Name = "Available Till")]
        [GreaterThan("Start_Hour",ErrorMessage = "End Hour should be greater than Start Hour")]
        public int End_Hour { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
