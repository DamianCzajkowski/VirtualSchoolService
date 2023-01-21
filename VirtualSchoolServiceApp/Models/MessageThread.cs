using System.ComponentModel.DataAnnotations;

namespace VirtualSchoolServiceApp.Models
{
    public class MessageThread
    {
        [Key]
        public int Id { get; set; }
        public string? Context { get; set; }
        public int? MessageId { get; set; }
        public Message Message { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser Author { get; set; }
    }

    public class MessageThreadVM
    {
		public string? Context { get; set; }
	}
}
