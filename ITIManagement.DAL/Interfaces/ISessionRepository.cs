using ITIManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.DAL.Interfaces
{
   
        public interface ISessionRepository
        {
            IEnumerable<Session> GetAll(string search, int pageNumber, int pageSize);
            Session GetById(int id);
            void Add(Session session);
            void Update(Session session);
            void Delete(int id);
        }
    
}
