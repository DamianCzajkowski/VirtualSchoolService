using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualSchoolServiceApp.Models;

namespace VirtualSchoolServiceApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ClassSubjects> ClassSubjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageThread> MessageThreads { get; set; }
        public DbSet<Annoucement> Annoucements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.AppUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Teacher)
                .WithOne(s => s.User)
                .HasForeignKey<Teacher>(s => s.AppUserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(a => a.Parent)
                .WithOne(p => p.User)
                .HasForeignKey<Parent>(s => s.AppUserId);

            modelBuilder.Entity<Teacher>()
                .HasOne(a => a.Class)
                .WithOne(s => s.Supervisor)
                .HasForeignKey<Class>(s => s.SupervisorId);

            modelBuilder.Entity<ClassSubjects>()
                .HasKey(t => new { t.SubjectId, t.ClassId });

            base.OnModelCreating(modelBuilder);
        }
    }
}