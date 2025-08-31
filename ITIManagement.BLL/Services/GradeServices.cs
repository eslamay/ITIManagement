using ITIManagement.BLL.ViewModels;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using System.Collections.Generic;
using System.Diagnostics;
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
            var grades = _gradeRepository.GetAll("", 1, int.MaxValue)
                .Where(g => g.SessionId == sessionId);

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId ?? 0,
                SessionName = g.Session != null ? g.Session.Course?.Name ?? $"Session {g.SessionId}" : $"Session {g.SessionId}",
                TraineeId = g.TraineeId,
                TraineeName = g.Trainee != null ? g.Trainee.Name : $"Trainee {g.TraineeId}",
                CourseName = g.Session?.Course?.Name ?? "Unknown"
            }).ToList();
        }

        public IEnumerable<GradeVM> GetGradesByTrainee(int traineeId)
        {
            var grades = _gradeRepository.GetAll("", 1, int.MaxValue)
                .Where(g => g.TraineeId == traineeId);

            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId ?? 0,
                SessionName = g.Session != null ? g.Session.Course?.Name ?? $"Session {g.SessionId}" : $"Session {g.SessionId}",
                TraineeId = g.TraineeId,
                TraineeName = g.Trainee != null ? g.Trainee.Name : $"Trainee {g.TraineeId}",
                CourseName = g.Session?.Course?.Category ?? "Unknown"
            }).ToList();
        }



        public IEnumerable<GradeVM> GetAllGrades()
        {
            var grades = _gradeRepository.GetAll("", 1, int.MaxValue);


            return grades.Select(g => new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                SessionId = g.SessionId ?? 0,
                SessionName = g.Session != null ? g.Session.Course?.Name ?? $"Session {g.SessionId}" : $"Session {g.SessionId}",
                TraineeId = g.TraineeId,
                TraineeName = g.Trainee != null ? g.Trainee.Name : $"Trainee {g.TraineeId}",
                CourseName = g.Session?.Course?.Category ?? "Unknown"
            }).ToList();
        }
        public void DeleteGrade(int id)
        {
           
            _gradeRepository.Delete(id);
        }
        public bool GradeExists(int traineeId, int sessionId)
        {
            return _gradeRepository.GetAll("", 1, int.MaxValue)
                             .Any(g => g.TraineeId == traineeId && g.SessionId == sessionId);
        }
        public GradeVM GetGradeById(int id)
        {
            var g = _gradeRepository.GetById(id);
            if (g == null) return null;

            return new GradeVM
            {
                Id = g.Id,
                Value = g.Value,
                TraineeId = g.TraineeId,
                SessionId = g.SessionId ?? 0,
                TraineeName = g.Trainee?.Name ?? "Unknown",
                CourseName = g.Session?.Course?.Category ?? "Unknown"
            };
        }
        public void UpdateGrade(GradeVM gradeVm)
        {
            var grade = _gradeRepository.GetById(gradeVm.Id);
            if (grade != null)
            {
                grade.Value = gradeVm.Value;
                grade.SessionId = gradeVm.SessionId;
                grade.TraineeId = gradeVm.TraineeId;
                _gradeRepository.Update(grade);
            }
        }
    }
}
