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
        public Student Student { get; set; }
        public int SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

    }
}
