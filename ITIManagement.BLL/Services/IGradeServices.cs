using ITIManagement.BLL.ViewModels;
using System.Collections.Generic;

namespace ITIManagement.BLL.Services
{
    public interface IGradeService
    {
        void RecordGrade(GradeVM gradeVm);
        IEnumerable<GradeVM> GetGradesBySession(int sessionId);
        IEnumerable<GradeVM> GetGradesByTrainee(int traineeId);
    }
}
