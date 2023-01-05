namespace VirtualSchoolServiceApp.Models
{
    public class ClassSubjects
    {
        public int ClassId { get; set; }
        public Class Class { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}