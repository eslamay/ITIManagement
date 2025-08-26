using ITIManagement.BLL.Services.CourseService;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;


namespace ITIManagement.UI.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;
        public CoursesController(ICourseRepository courseRepository, ICourseService courseService)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
        }

        public IActionResult Index(int page = 1, int? pageSize = null, string? searchName = null)
        {
            var result = _courseService.GetPaged(page, pageSize ?? 5, searchName);
            ViewBag.SearchName = searchName;
            return View(result);
        }
        public IActionResult Details(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.Add(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }
        [HttpPost]
     
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseService.Update(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
            return View(course);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult AssignInstructor(int courseId, int instructorId)
        {
            _courseService.AssignInstructor(courseId, instructorId);
            return RedirectToAction(nameof(Index));
        }

        // Remote Validation
        public JsonResult IsCourseNameAvailable(string name, int id = 0)
        {
            var course = _courseRepository.GetByName(name);
            if (course != null && course.Id != id)
                return Json($"Course name '{name}' is already in use.");

            return Json(true);
        }
    }
}
