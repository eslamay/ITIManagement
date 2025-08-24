using System.Collections.Generic;
using ITIManagement.BLL.ViewModels;

namespace ITIManagement.BLL.Services
{
    public interface ISessionService
    {
        IEnumerable<SessionVM> GetAll(string search, int pageNumber, int pageSize);
        IEnumerable<SessionVM> GetAll(); 
        SessionVM? GetById(int id);
        void Add(SessionVM sessionVm);
        void Update(SessionVM sessionVm);
        void Delete(int id);
    }
}
