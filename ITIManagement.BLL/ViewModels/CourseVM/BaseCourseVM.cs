using ITIManagement.BLL.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.BLL.ViewModels.CourseVM
{
	public class BaseCourseVM
	{
		[Required]
		[StringLength(50, MinimumLength = 3)]
		[NoNumbers]
		[Remote(action: "IsCourseNameAvailable", controller: "Course", ErrorMessage = "Course name already exists.")]
		public string Name { get; set; } = default!;

		[Required]
		public string Category { get; set; } = default!;

		public int? InstructorId { get; set; }
	}
}
