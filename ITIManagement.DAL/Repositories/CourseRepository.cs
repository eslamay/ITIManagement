using ITIManagement.DAL.Data;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.DAL.Repositories
{
    public class CourseRepository : ICourseRepository
    {
   
            private readonly AppDbContext _context;

            public CourseRepository(AppDbContext context)
            {
                _context = context;
            }

        public IEnumerable<Course> GetAll(string search, int pageNumber, int pageSize)
        {
            return _context.Courses
                .Where(c => string.IsNullOrEmpty(search) || c.Name.Contains(search))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


        public Course GetById(int id)
            {
                return _context.Courses.Find(id);
            }

            public Course GetByName(string name)
            {
                return _context.Courses.FirstOrDefault(c => c.Name == name);
            }

            public void Add(Course course)
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
            }

            public void Update(Course course)
            {
                _context.Courses.Update(course);
                _context.SaveChanges();
            }

            public void Delete(int id)
            {
                var course = _context.Courses.Find(id);
                if (course != null)
                {
                    _context.Courses.Remove(course);
                    _context.SaveChanges();
                }
            }
        public int GetCount(string? search)
        {
            return _context.Courses
                .Where(c => string.IsNullOrEmpty(search) || c.Name.Contains(search))
                .Count();
        }

    }
}

