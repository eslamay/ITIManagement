using ITIManagement.BLL.Services;
using ITIManagement.BLL.Services.CourseService;
using ITIManagement.BLL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace ITIManagement.UI.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ICourseService _courseService; 

        public SessionController(ISessionService sessionService, ICourseService courseService)
        {
            _sessionService = sessionService;
            _courseService = courseService;
        }

        public IActionResult Index(string search = "", int page = 1, int pageSize = 10)
        {
            var allSessions = _sessionService.GetAll(search, 1, int.MaxValue).ToList();
            var pagedSessions = _sessionService.GetAll(search, page, pageSize).ToList();

            var model = new ITIManagement.BLL.Pagination.PageResult<SessionVM>
            {
                Items = pagedSessions,
                Page = page,
                PageSize = pageSize,
                TotalCount = allSessions.Count
            };

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var session = _sessionService.GetById(id);
            if (session == null)
                return NotFound();

            return View(session);
        }

        public IActionResult Create()
        {
            var vm = new SessionVM
            {
                Courses = _courseService.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SessionVM sessionVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sessionService.Add(sessionVm);
                    TempData["SuccessMessage"] = "Session created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "An error occurred while creating the session!";
                }
            }

           
            sessionVm.Courses = _courseService.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(sessionVm);
        }

        public IActionResult Edit(int id)
        {
            var session = _sessionService.GetById(id);
            if (session == null)
                return NotFound();

            session.Courses = _courseService.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SessionVM sessionVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sessionService.Update(sessionVm);
                    TempData["SuccessMessage"] = "Session updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "Error occurred while updating the session!";
                }
            }

           
            sessionVm.Courses = _courseService.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            return View(sessionVm);
        }

        public IActionResult Delete(int id)
        {
            var session = _sessionService.GetById(id);
            if (session == null)
                return NotFound();

            return View(session);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _sessionService.Delete(id);
                TempData["SuccessMessage"] = "Session deleted successfully!";
            }
            catch
            {
                TempData["ErrorMessage"] = "Error occurred while deleting the session!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}













