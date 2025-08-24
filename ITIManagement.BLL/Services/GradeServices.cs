using ITIManagement.BLL.ViewModels;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace ITIManagement.BLL.Services
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            _gradeRepository = gradeRepository;
        }

        public void RecordGrade(GradeVM gradeVm)
        {
            var grade = new Grade
            {
                Value = gradeVm.Value,
                SessionId = gradeVm.SessionId,
                TraineeId = gradeVm.TraineeId
            };

            _gradeRepository.Add(grade);
        }

        public IEnumerable<GradeVM> GetGradesBySession(int sessionId)
        {
            var grades = _gradeRepository
                .GetAll("", 1, int.MaxValue) 
                .Where(g => g.SessionId == sessionId);

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId ?? 0,
                TraineeId = g.TraineeId,
                TraineeName = g.Trainee != null ? g.Trainee.Name : null
            }).ToList();
        }

        public IEnumerable<GradeVM> GetGradesByTrainee(int traineeId)
        {
            var grades = _gradeRepository
                .GetAll("", 1, int.MaxValue) 
                .Where(g => g.TraineeId == traineeId);

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId ?? 0,
                CourseName = g.Session != null ? g.Session.Course!.Name : null
            }).ToList();
        }
    }
}
