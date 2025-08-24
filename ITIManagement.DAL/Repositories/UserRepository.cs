using ITIManagement.DAL.Data;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll(string search, int pageNumber, int pageSize)
        {
            var query = _context.Users.Include(u=>u.Courses).Include(u=>u.Grades).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Name.Contains(search));
            }

            return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

		public int GetCount(string? searchName = null)
		{
			var query = _context.Users.AsNoTracking();
			if (!string.IsNullOrEmpty(searchName))
			{
				query = query.Where(x => x.Name.Contains(searchName));
			}
			return query.Count();
		}

		public User GetById(int id)
        {
            return _context.Users.Include(u => u.Courses).Include(u => u.Grades).FirstOrDefault(u => u.Id == id);
        }

		public User GetByEmail(string email)
		{
			return _context.Users.FirstOrDefault(u => u.Email == email);
		}

		public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
