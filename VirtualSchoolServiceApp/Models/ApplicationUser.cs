using Microsoft.AspNetCore.Identity;

namespace VirtualSchoolServiceApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FirstName { get; set; }
        [PersonalData]
        public string? LastName { get; set; }
        public Student? Student { get; set; }
        public bool IsStudent { get; set; } = false;
        public Teacher? Teacher { get; set; }
        public bool IsTeacher { get; set; } = false;
        public Parent? Parent { get; set; }
        public bool IsParent { get; set; } = false;

        public List<MessageThread> MessageThreads { get; set; } = new List<MessageThread>();

    }

    public class ApplicationUserVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsStudent { get; set; } = false;
    }
}
