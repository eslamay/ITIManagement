using ITIManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.DAL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll(string search, int pageNumber, int pageSize);
        User GetById(int id);
		int GetCount(string? searchName = null);
		User GetByEmail(string email);
		void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
