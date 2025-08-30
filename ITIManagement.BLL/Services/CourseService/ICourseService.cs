using ITIManagement.BLL.Pagination;
using ITIManagement.BLL.ViewModels.CourseVM;
using ITIManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITIManagement.BLL.Services.CourseService
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAll();
        CourseVM? GetById(int id);
        bool Add(CreateCourseVM course);
        void Update(EditCourseVM course);
        void Delete(int id);
        PageResult<CourseVM> GetPaged(int page, int pageSize, string? searchName);
        void AssignInstructor(int courseId, int instructorId);

		IEnumerable<SelectListItem> GetInstructorsSelectList();

	}
}
