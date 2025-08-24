using ITIManagement.DAL.Data;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ITIManagement.DAL.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;

        public GradeRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Grade> GetAll(string search, int pageNumber, int pageSize)
        {
            var query = _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                
                query = query.Where(g =>
                    g.Value.ToString().Contains(search) ||
                    (g.Trainee != null && g.Trainee.
                    Name.Contains(search))
                );
            }

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public Grade GetById(int id)
        {
            return _context.Grades
                .Include(g => g.Session)
                .Include(g => g.Trainee)
                .FirstOrDefault(g => g.Id == id);
        }

        public void Add(Grade grade)
        {
            _context.Grades.Add(grade);
            _context.SaveChanges();
        }

        public void Update(Grade grade)
        {
            _context.Grades.Update(grade);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var grade = _context.Grades.Find(id);
            if (grade != null)
            {
                _context.Grades.Remove(grade);
                _context.SaveChanges();
            }
        }
    }
}
