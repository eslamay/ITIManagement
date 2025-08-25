
using ITIManagement.BLL.Services;
using ITIManagement.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ITIManagement.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        public IActionResult BySession(int sessionId)
        {
            var grades = _gradeService.GetGradesBySession(sessionId);
            return View(grades); // لازم تعمل View بنفس الاسم
        }

        
        public IActionResult ByTrainee(int traineeId)
        {
            var grades = _gradeService.GetGradesByTrainee(traineeId);
            return View(grades); // لازم تعمل View بنفس الاسم
        }

      
        [HttpGet]
        public IActionResult Record()
        {
            return View();
        }

        public IActionResult Record(GradeVM gradeVm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.RecordGrade(gradeVm);
                return RedirectToAction("BySession", new { sessionId = gradeVm.SessionId });
            }
            return View(gradeVm);
        }
    }
}
