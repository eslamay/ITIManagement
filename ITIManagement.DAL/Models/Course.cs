using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;


namespace ITIManagement.DAL.Models
{
	public class Course
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3)]
        [Remote(action: "IsCourseNameAvailable", controller: "Courses", ErrorMessage = "Course name already exists.")]
        public string Name { get; set; }=default!;

		[Required]
		public string Category { get; set; }=default!;

		[Required]
		[ForeignKey("Instructor")]
		public int? InstructorId { get; set; }
		public User? Instructor { get; set; }

		public ICollection<Session> Sessions { get; set; }= new List<Session>();
	}
}
