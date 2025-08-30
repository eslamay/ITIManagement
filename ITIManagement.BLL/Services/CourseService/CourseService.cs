using ITIManagement.BLL.Pagination;
using ITIManagement.BLL.ViewModels.CourseVM;
using ITIManagement.DAL.Data;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using ITIManagement.DAL.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public CourseVM? GetById(int id)
        {
            var course = _courseRepository.GetById(id);

			return course != null ? new CourseVM
			{
				Id = course.Id,
				Name = course.Name,
				Category = course.Category,
				InstructorName = course.Instructor != null ? course.Instructor.Name : "Not Assigned"
			} : null;
        }

        public bool Add(CreateCourseVM courseVM)
        {

			var existing = _courseRepository.GetByName(courseVM.Name);
			if (existing != null)
				return false;

			var course = new Course
			{
				Name = courseVM.Name,
				Category = courseVM.Category,
				InstructorId = courseVM.InstructorId
			};
			_courseRepository.Add(course);

			return true;
        }

		public void Update(EditCourseVM vm)
		{
			var course = _courseRepository.GetById(vm.Id);
			if (course != null)
			{
				course.Name = vm.Name;
				course.Category = vm.Category;
				course.InstructorId = vm.InstructorId;
				_courseRepository.Update(course);
			}
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

		public IEnumerable<SelectListItem> GetInstructorsSelectList()
		{
			return _context.Users
				.Where(u => u.Role == UserRole.Instructor)
				.Select(u => new SelectListItem
				{
					Value = u.Id.ToString(),
					Text = u.Name
				})
				.ToList();
		}

	}
}