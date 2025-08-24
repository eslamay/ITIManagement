using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using global::ITIManagement.BLL.ViewModels;
using global::ITIManagement.DAL.Data;
using global::ITIManagement.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace ITIManagement.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly AppDbContext _context;

        public GradeService(AppDbContext context)
        {
            _context = context;
        }

        public void RecordGrade(GradeVM gradeVm)
        {
            var grade = new Grade
            {
                Value = gradeVm.Value,
                SessionId = gradeVm.SessionId,
                TraineeId = gradeVm.TraineeId
            };

            _context.Grades.Add(grade);
            _context.SaveChanges();
        }

        public IEnumerable<GradeVM> GetGradesBySession(int sessionId)
        {
            return _context.Grades
                .Where(g => g.SessionId == sessionId)
                .Select(g => new GradeVM
                {
                    Id = g.Id,
                    Value = g.Value,
                    SessionId = g.SessionId ?? 0,
                    TraineeId = g.TraineeId,
                    TraineeName = g.Trainee != null ? g.Trainee.Name : null
                })
                .ToList();
        }

        public IEnumerable<GradeVM> GetGradesByTrainee(int traineeId)
        {
            return _context.Grades
                .Where(g => g.TraineeId == traineeId)
                .Select(g => new GradeVM
                {
                    Id = g.Id,
                    Value = g.Value,
                    SessionId = g.SessionId ?? 0,
                    CourseName = g.Session != null ? g.Session.Course!.Name : null
                })
                .ToList();
        }
    }
}




