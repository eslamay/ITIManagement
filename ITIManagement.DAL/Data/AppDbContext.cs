using ITIManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace ITIManagement.DAL.Data
{
	public class AppDbContext:DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

		public DbSet<Course> Courses { get; set; }
		public DbSet<Session> Sessions { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Grade> Grades { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Course>()
				.HasIndex(c => c.Name)
				.IsUnique();

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();

			modelBuilder.Entity<Course>()
				.HasOne(c => c.Instructor)
				.WithMany(u => u.Courses)
				.HasForeignKey(c => c.InstructorId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Session>()
				.HasOne(s => s.Course)
				.WithMany(c => c.Sessions)
				.HasForeignKey(s => s.CourseId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Grade>()
				.HasOne(g => g.Session)
				.WithMany(s => s.Grades)
				.HasForeignKey(g => g.SessionId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Grade>()
				.HasOne(g => g.Trainee)
				.WithMany(u => u.Grades)
				.HasForeignKey(g => g.TraineeId)
				.OnDelete(DeleteBehavior.Cascade);

			// Users
			modelBuilder.Entity<User>().HasData(
				new User { Id = 1, Name = "Admin User", Email = "admin@example.com", Role = UserRole.Admin },
				new User { Id = 2, Name = "John Instructor", Email = "john@instructor.com", Role = UserRole.Instructor },
				new User { Id = 3, Name = "Mary Instructor", Email = "mary@instructor.com", Role = UserRole.Instructor },
				new User { Id = 4, Name = "Ali Trainee", Email = "ali@trainee.com", Role = UserRole.Trainee },
				new User { Id = 5, Name = "Sara Trainee", Email = "sara@trainee.com", Role = UserRole.Trainee }
			);

			// Courses
			modelBuilder.Entity<Course>().HasData(
				new Course { Id = 1, Name = "C# Fundamentals", Category = "Programming", InstructorId = 2 },
				new Course { Id = 2, Name = "ASP.NET Core MVC", Category = "Web Development", InstructorId = 3 },
				new Course { Id = 3, Name = "Database Design", Category = "Databases", InstructorId = 3 },
				new Course { Id = 4, Name = "OOP Concepts", Category = "Programming", InstructorId = 3 },
				new Course { Id = 5, Name = "Frontend Basics", Category = "Web Development", InstructorId = 2 }
			);

			// Sessions
			modelBuilder.Entity<Session>().HasData(
				new Session { Id = 1, CourseId = 1, StartDate = new DateTime(2025, 08, 21), EndDate = new DateTime(2025, 08, 31) },
				new Session { Id = 2, CourseId = 2, StartDate = new DateTime(2025, 08, 22), EndDate = new DateTime(2025, 09, 01) },
				new Session { Id = 3, CourseId = 3, StartDate = new DateTime(2025, 08, 23), EndDate = new DateTime(2025, 09, 02) },
				new Session { Id = 4, CourseId = 4, StartDate = new DateTime(2025, 08, 24), EndDate = new DateTime(2025, 09, 03) },
				new Session { Id = 5, CourseId = 5, StartDate = new DateTime(2025, 08, 25), EndDate = new DateTime(2025, 09, 04) }
			);

			// Grades
			modelBuilder.Entity<Grade>().HasData(
				new Grade { Id = 1, SessionId = 1, TraineeId = 4, Value = 85 },
				new Grade { Id = 2, SessionId = 1, TraineeId = 5, Value = 90 },
				new Grade { Id = 3, SessionId = 2, TraineeId = 4, Value = 78 },
				new Grade { Id = 4, SessionId = 3, TraineeId = 5, Value = 88 },
				new Grade { Id = 5, SessionId = 4, TraineeId = 4, Value = 95 }
			);
		}
	}
}
