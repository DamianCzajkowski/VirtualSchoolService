using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualSchoolServiceApp.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public ApplicationUser User { get; set; }
        public Class? Class { get; set; }
        public List<Subject> Subjects { get; set; } = new List<Subject>();

    }
}
