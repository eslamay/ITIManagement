using ITIManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.DAL.Interfaces
{
    public interface IGradeRepository
    {
        IEnumerable<Grade> GetAll(string search, int pageNumber, int pageSize);
        Grade GetById(int id);
        void Add(Grade grade);
        void Update(Grade grade);
        void Delete(int id);
    }
}
