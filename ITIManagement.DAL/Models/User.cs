using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;



namespace ITIManagement.DAL.Models
{
	public enum UserRole
	{
		Admin = 1,
		Instructor = 2,
		Trainee = 3
	}
	public class User
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }=default!;

		[Required]
		[EmailAddress]
		[Remote(action: "IsEmailAvailable", controller: "Users", AdditionalFields = "Id", ErrorMessage = "Email already exists.")]
		public string Email { get; set; }=default!;

		[Required]
		public UserRole Role { get; set; }
		
		public ICollection<Course>? Courses { get; set; }=new List<Course>();

		public ICollection<Grade>? Grades { get; set; } = new List<Grade>();
	}
}
