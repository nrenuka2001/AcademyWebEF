using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AcademyWebEF.Models
{
    public class StudentEditorModel
    {
        [Required(ErrorMessage = "Please enter name for the student")]
        [StringLength(50)]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }=string.Empty;

        [Required(ErrorMessage = "Please enter roll no")]
        [StringLength(20)]
        [Display(Name = "Roll No")]
        public string RollNo { get; set; }= string.Empty;

        [Display(Name = "Mobile No")]
        [StringLength(20)]
        public string? Mobile { get; set; }

        [EmailAddress]
        [StringLength(20)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [HiddenInput]
        public int StudentID { get; set; }

        [Required(ErrorMessage = "Please select a course")]
        [Display(Name = "Course")]
        public int CourseID { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Courses { get; set; }=new List<SelectListItem>();

        [Required(ErrorMessage = "Please select a nationality")]
        [Display(Name = "Nationality")]
        public int NationalityId { get; set; }

        [IgnoreDataMember]
        public List<SelectListItem> Nationalities { get; set; } = new List<SelectListItem>();
    }
}