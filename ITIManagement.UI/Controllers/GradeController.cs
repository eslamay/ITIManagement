using ITIManagement.BLL.Services;
using ITIManagement.BLL.ViewModels;
using ITIManagement.DAL.Data;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ITIManagement.Web.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeService _gradeService;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly AppDbContext _context;

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
                _gradeService.UpdateGrade(gradeVm); 
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
            _gradeService.DeleteGrade(Id); 
            return RedirectToAction("AllGrades");
        }
        public IActionResult BySession(int? sessionId)
        {
            var grades = _gradeService.GetAllGrades();

            if (sessionId.HasValue)
            {
                grades = grades.Where(g => g.SessionId == sessionId.Value).ToList();
            }

            return View(grades);
        }

        public IActionResult ByTrainee()
        {
           
            var grades = _gradeService.GetAllGrades().ToList();

           
            return View("ByTrainee", grades);
        }
        public IActionResult AllGrades(string searchName, int pageNumber = 1, int pageSize = 5)
        {
            var grades = _gradeService.GetAllGrades().AsQueryable();

            // البحث بالاسم
            if (!string.IsNullOrEmpty(searchName))
            {
                grades = grades.Where(g => g.TraineeName.Contains(searchName));
            }

            // عدد الصفحات
            int totalItems = grades.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            // احضار البيانات للصفحة الحالية
            var pagedGrades = grades
                                .Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            // إرسال البيانات مع المعلومات المطلوبة للفيو
            ViewBag.SearchName = searchName;
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(pagedGrades);
        }




    }
}
