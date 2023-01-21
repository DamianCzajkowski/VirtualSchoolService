using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualSchoolServiceApp.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Mark { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int StudentId { get; set; }
        [ForeignKey("StudentId")]
        [ValidateNever]
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [ValidateNever]
        public Subject Subject { get; set; }

    }

    public class GradeVM
    {
        public Student Student { get; set; }
        public List<Grade> Grades { get; set; }
        public Subject Subject { get; set; }
    }
}
