using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace VirtualSchoolServiceApp.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ClassSubjects> ClassSubjects { get; set; } = new List<ClassSubjects>();
        [ValidateNever]
        public string ContentOfEducation { get; set; }

    }
}
