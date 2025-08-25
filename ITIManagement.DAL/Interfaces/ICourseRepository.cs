using ITIManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace ITIManagement.DAL.Interfaces
{
    public interface ICourseRepository
    {

        IEnumerable<Course> GetAll(string search, int pageNumber, int pageSize);
        Course GetById(int id);
        Course GetByName(string name);
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
        int GetCount(string? search);
    }
}
