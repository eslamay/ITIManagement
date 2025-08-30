using ITIManagement.BLL.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.BLL.ViewModels.CourseVM
{
	public class EditCourseVM : BaseCourseVM
	{
		public int Id { get; set; }
	}
}
