using ITIManagement.BLL.Pagination;
using ITIManagement.BLL.ViewModels.Course;
using ITIManagement.DAL.Models;
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
        Course? GetById(int id);
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
        PageResult<CourseVM> GetPaged(int page, int pageSize, string? searchName);
        void AssignInstructor(int courseId, int instructorId);
      
    }
}
