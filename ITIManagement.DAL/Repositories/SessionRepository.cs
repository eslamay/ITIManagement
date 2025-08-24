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
    internal class SessionRepository : ISessionRepository
    {

      
            private readonly AppDbContext _context;

            public SessionRepository(AppDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Session> GetAll(string search, int pageNumber, int pageSize)
            {
                return _context.Sessions
                    .Include(s => s.Course) 
                    .Where(s => string.IsNullOrEmpty(search) || s.Course.Name.Contains(search))
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            public Session GetById(int id)
            {
                return _context.Sessions
                    .Include(s => s.Course)
                    .FirstOrDefault(s => s.Id == id);
            }

            public void Add(Session session)
            {
                _context.Sessions.Add(session);
                _context.SaveChanges();
            }

            public void Update(Session session)
            {
                _context.Sessions.Update(session);
                _context.SaveChanges();
            }

            public void Delete(int id)
            {
                var session = _context.Sessions.Find(id);
                if (session != null)
                {
                    _context.Sessions.Remove(session);
                    _context.SaveChanges();
                }
            }
    }

    
} 


