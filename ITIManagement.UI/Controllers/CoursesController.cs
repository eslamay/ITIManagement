using ITIManagement.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ITIManagement.UI.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create() => View();

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
