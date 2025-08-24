using ITIManagement.DAL.Models;

namespace ITIManagement.BLL.ViewModels.UserVM
{
	public class UserDetailsVM
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Email { get; set; } = default!;
		public UserRole Role { get; set; }

		public ICollection<Course> Courses { get; set; } = new List<Course>();

		public ICollection<Grade> Grades { get; set; } = new List<Grade>();
	}
}
