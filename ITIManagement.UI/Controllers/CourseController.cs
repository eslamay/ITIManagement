using ITIManagement.BLL.Services.CourseService;
using ITIManagement.BLL.ViewModels.CourseVM;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ITIManagement.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ICourseService _courseService;
        public CourseController(ICourseRepository courseRepository, ICourseService courseService)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
        }

        public IActionResult Index(int page = 1, int pageSize = 5, string? searchName = null)
        {
            var result = _courseService.GetPaged(page, pageSize, searchName);
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
			ViewBag.Instructors = _courseService.GetInstructorsSelectList();
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateCourseVM vm)
		{
			if (ModelState.IsValid)
			{
				var result = _courseService.Add(vm);
				if (!result)
				{
					ModelState.AddModelError("Name", "Course name already exists.");
					ViewBag.Instructors = _courseService.GetInstructorsSelectList();
					return View(vm);
				}

				TempData["SuccessMessage"] = "Course has been created successfully!";
				return RedirectToAction(nameof(Index));
			}

			ViewBag.Instructors = _courseService.GetInstructorsSelectList();
			return View(vm);
		}


		[HttpGet]
		public IActionResult Edit(int id)
		{
			var course = _courseService.GetById(id);
			if (course == null) return NotFound();

			var vm = new EditCourseVM
			{
				Id = course.Id,
				Name = course.Name,
				Category = course.Category,
				InstructorId = course.InstructorId
			};

			ViewBag.Instructors = _courseService.GetInstructorsSelectList();
			return View(vm);
		}

		[HttpPost]
		public IActionResult Edit(EditCourseVM vm)
		{
			if (ModelState.IsValid)
			{
				_courseService.Update(vm);
				TempData["SuccessMessage"] = "Course has been updated successfully!";
				return RedirectToAction(nameof(Index));
			}
			ViewBag.Instructors = _courseService.GetInstructorsSelectList();
			return View(vm);
		}
		[HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);
            if (course == null) return NotFound();
			TempData["SuccessMessage"] = "Course deleted successfully!";
			return RedirectToAction(nameof(Index));
		}
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _courseService.Delete(id);
            TempData["SuccessMessage"] = "Course deleted successfully!";
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
