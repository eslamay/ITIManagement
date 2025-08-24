using ITIManagement.BLL.ViewModels;
using ITIManagement.DAL.Data;
using ITIManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITIManagement.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly AppDbContext _context;

        public SessionService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SessionVM> GetAll()
        {
            return _context.Sessions
                           .Select(s => new SessionVM
                           {
                               Id = s.Id,
                               CourseId = s.CourseId ?? 0,
                               CourseName = s.Course != null ? s.Course.Name : string.Empty,
                               StartDate = s.StartDate,
                               EndDate = s.EndDate
                           })
                           .ToList();
        }

        public SessionVM? GetById(int id)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return null;

            return new SessionVM
            {
                Id = session.Id,
                CourseId = session.CourseId ?? 0,
                CourseName = session.Course != null ? session.Course.Name : string.Empty,
                StartDate = session.StartDate,
                EndDate = session.EndDate
            };
        }

        public void Add(SessionVM sessionVm)
        {
            var session = new Session
            {
                CourseId = sessionVm.CourseId,
                StartDate = sessionVm.StartDate,
                EndDate = sessionVm.EndDate
            };

            _context.Sessions.Add(session);
            _context.SaveChanges();
        }

        public void Update(SessionVM sessionVm)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == sessionVm.Id);
            if (session == null) return;

            session.CourseId = sessionVm.CourseId;
            session.StartDate = sessionVm.StartDate;
            session.EndDate = sessionVm.EndDate;

            _context.Sessions.Update(session);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var session = _context.Sessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return;

            _context.Sessions.Remove(session);
            _context.SaveChanges();
        }
    }
}
