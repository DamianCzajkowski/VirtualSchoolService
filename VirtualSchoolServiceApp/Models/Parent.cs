using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualSchoolServiceApp.Models
{
    public class Parent
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public ApplicationUser User { get; set; }
        public List<Student>? Kids { get; set; } = new List<Student>();
    }
}
