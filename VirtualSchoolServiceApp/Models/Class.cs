using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace VirtualSchoolServiceApp.Models
{
    public class Class
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> Students { get; set; } = new List<Student>();
        public List<ClassSubjects> ClassSubjects { get; set; } = new List<ClassSubjects>();
        public int? SupervisorId { get; set; }
        public Teacher? Supervisor { get; set; }
    }

    public class AddStudentClassVM
    {
        public Class Class { get; set; }
        public List<int>? Students { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> StudentList { get; set; }
    }

    public class AddSubjectClassVM
    {
        public Class Class { get; set; }
        public List<int>? Subjects { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> SubjectList { get; set; }
    }
}
