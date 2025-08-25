using ITIManagement.BLL.Pagination;
using ITIManagement.BLL.ViewModels.CourseVM;
using ITIManagement.DAL.Data;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using ITIManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.BLL.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly AppDbContext _context;

        public CourseService(ICourseRepository courseRepository, AppDbContext context)
        {
            _courseRepository = courseRepository;
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.Include(c => c.Instructor).ToList();
        }

        public Course? GetById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public void Add(Course course)
        {
            _courseRepository.Add(course);
        }

        public void Update(Course course)
        {
            _courseRepository.Update(course);
        }

        public void Delete(int id)
        {
            _courseRepository.Delete(id);
        }


        PageResult<CourseVM> ICourseService.GetPaged(int page, int pageSize, string? searchName)
        {
            var items = _courseRepository.GetAll(searchName!, page, pageSize)
               .Select(c => new CourseVM
               {
                   Id = c.Id,
                   Name = c.Name,
                   Category = c.Category,
                   InstructorName = c.Instructor != null ? c.Instructor.Name : "Not Assigned"
               }).ToList();

            var totalCount = _courseRepository.GetCount(searchName);

            return new PageResult<CourseVM>
            {
                Items = items,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }

       
        void ICourseService.AssignInstructor(int courseId, int instructorId)
        {
            var course = _context.Courses.Find(courseId);
            if (course != null)
            {
                course.InstructorId = instructorId;
                _context.SaveChanges();
            }
        }

    }
}