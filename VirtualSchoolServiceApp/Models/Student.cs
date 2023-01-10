using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualSchoolServiceApp.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public ApplicationUser User { get; set; }

        public int? ClassId { get; set; }
        [ForeignKey("ClassId")]
        public Class? Class { get; set; }
        public List<Grade>? Grades { get; set; } = new List<Grade>();

    }
    public class StudentVM
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
    }

}
