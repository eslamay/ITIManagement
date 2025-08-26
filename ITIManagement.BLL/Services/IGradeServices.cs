using ITIManagement.BLL.ViewModels;
using System.Collections.Generic;

namespace ITIManagement.BLL.Services
{
    public interface IGradeService
    {
        void RecordGrade(GradeVM gradeVm);
        IEnumerable<GradeVM> GetGradesBySession(int sessionId);
        IEnumerable<GradeVM> GetGradesByTrainee(int traineeId);
        IEnumerable<GradeVM> GetAllGrades();
       void UpdateGrade(GradeVM gradeVm);
        void DeleteGrade(int id);
        GradeVM GetGradeById(int id); 
    }
}
