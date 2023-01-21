using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualSchoolServiceApp.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string Context { get; set; }
        public string Title { get; set; }
        public int? ParentId { get; set; }
        public Parent Parent { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }
        public string? Status { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public List<MessageThread>? MessageThreads { get; set; } = new List<MessageThread>();
    }

    public class MessageVM
    {
        public string Context { get; set; }
        public string Title { get; set; }
        public int? TeacherId { get; set; }
    }
}
