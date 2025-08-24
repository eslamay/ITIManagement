using ITIManagement.BLL.ViewModels;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace ITIManagement.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        
        public IEnumerable<SessionVM> GetAll(string search, int pageNumber, int pageSize)
        {
            var sessions = _sessionRepository.GetAll(search, pageNumber, pageSize);

            return sessions.Select(s => new SessionVM
            {
                Id = s.Id,
                CourseId = s.CourseId ?? 0,
                CourseName = s.Course != null ? s.Course.Name : string.Empty,
                StartDate = s.StartDate,
                EndDate = s.EndDate
            }).ToList();
        }

        
        public IEnumerable<SessionVM> GetAll()
        {
            return GetAll("", 1, int.MaxValue);
        }

        public SessionVM? GetById(int id)
        {
            var session = _sessionRepository.GetById(id);
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

            _sessionRepository.Add(session);
        }

        public void Update(SessionVM sessionVm)
        {
            var session = new Session
            {
                Id = sessionVm.Id,
                CourseId = sessionVm.CourseId,
                StartDate = sessionVm.StartDate,
                EndDate = sessionVm.EndDate
            };

            _sessionRepository.Update(session);
        }

        public void Delete(int id)
        {
            _sessionRepository.Delete(id);
        }
    }
}
