using System.ComponentModel.DataAnnotations;

namespace VirtualSchoolServiceApp.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ClassSubjects> ClassSubjects { get; set; } = new List<ClassSubjects>();

    }
}
