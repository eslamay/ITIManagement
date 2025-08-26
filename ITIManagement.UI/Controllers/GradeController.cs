using ITIManagement.BLL.Services;
using ITIManagement.BLL.ViewModels;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ITIManagement.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;

        public GradeController(IGradeService gradeService, ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _gradeService = gradeService;
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Record()
        {
            ViewBag.Trainees = _userRepository.GetAll("", 1, int.MaxValue)
                                              .Where(u => u.Role == UserRole.Trainee)
                                              .ToList();

            ViewBag.Sessions = _sessionRepository.GetAll("", 1, int.MaxValue).ToList();

            return View(new GradeVM());
        }

        [HttpPost]
        public IActionResult Record(GradeVM gradeVm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.RecordGrade(gradeVm);
                return RedirectToAction("BySession", new { sessionId = gradeVm.SessionId });
            }

            // لو فيه خطأ نرجع نفس الفورم مع القيم
            ViewBag.Trainees = _userRepository.GetAll("", 1, int.MaxValue)
                                              .Where(u => u.Role == UserRole.Trainee)
                                              .ToList();
            ViewBag.Sessions = _sessionRepository.GetAll("", 1, int.MaxValue).ToList();

            return View(gradeVm);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var grade = _gradeService.GetGradeById(id);
            if (grade == null)
                return NotFound();

            ViewBag.Trainees = _userRepository.GetAll("", 1, int.MaxValue)
                                              .Where(u => u.Role == UserRole.Trainee)
                                              .ToList();

            ViewBag.Sessions = _sessionRepository.GetAll("", 1, int.MaxValue).ToList();

            return View(grade);
        }

        [HttpPost]
        public IActionResult Edit(GradeVM gradeVm)
        {
            if (ModelState.IsValid)
            {
                _gradeService.UpdateGrade(gradeVm); // بدل RecordGrade
                return RedirectToAction("AllGrades");
            }

            ViewBag.Sessions = _sessionRepository.GetAll("", 1, int.MaxValue);
            ViewBag.Trainees = _userRepository.GetAll("", 1, int.MaxValue)
                                              .Where(u => u.Role == UserRole.Trainee)
                                              .ToList();
            return View(gradeVm);
        }
        // GET: Delete Grade
        public IActionResult Delete(int id)
        {
            var grade = _gradeService.GetAllGrades().FirstOrDefault(g => g.Id == id);
            if (grade == null)
                return NotFound();

            return View(grade);
        }

        // POST: Confirm Delete
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            _gradeService.DeleteGrade(Id); // لازم تعمل دالة DeleteGrade في السيرفس
            return RedirectToAction("AllGrades");
        }
        public IActionResult BySession()
        {
          
            var grades = _gradeService.GetAllGrades().ToList();

            return View(grades);
        }
        public IActionResult ByTrainee()
        {
           
            var grades = _gradeService.GetAllGrades().ToList();

           
            return View("ByTrainee", grades);
        }
        public IActionResult AllGrades()
        {
            var grades = _gradeService.GetAllGrades();
            return View(grades);
        }
    }
}
