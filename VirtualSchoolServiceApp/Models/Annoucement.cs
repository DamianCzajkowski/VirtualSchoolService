using System.ComponentModel.DataAnnotations;

namespace VirtualSchoolServiceApp.Models
{
    public class Annoucement
    {
        [Key]
        public int Id { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set;}
        public string Content { get; set; }
        public string Title { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow; 
    }

    public class AnnoucementVM
    {
        public string Content { get; set; }
        public string Title { get; set; }
    }

}
