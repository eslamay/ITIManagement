using ITIManagement.BLL.Services.CourseService;
using ITIManagement.BLL.Services.UserServices;
using ITIManagement.BLL.ViewModels.CourseVM;
using ITIManagement.BLL.ViewModels.UserVM;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;
using ITIManagement.UI.Configurations;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ITIManagement.UI.Controllers
{
    public class UsersController : Controller
    {       
        private readonly IUserRepository _userRepository;
		private readonly IUserService userService;
		private readonly IOptionsMonitor<UserSettings> options;

		public UsersController(IUserRepository userRepository,IUserService userService,
			IOptionsMonitor<UserSettings> options)
        {
            _userRepository = userRepository;
			this.userService = userService;
			this.options = options;
		}

		public IActionResult Index(int page = 1, string? searchName = null)
		{
			int pageSize = options.CurrentValue.DefaultPageSize;
			var users = userService.GetAllPage(page, pageSize, searchName);
			ViewBag.Roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
			ViewBag.SearchName = searchName;
			return View(users);
		}

		public IActionResult GetById(int id)
		{
			var user = userService.GetById(id);
			if (user == null)
				return NotFound();

			return View("Details", user);
		}

		[HttpGet]
		public IActionResult Create()
		{
            ViewBag.Roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateUserVM model)
		{
			if (ModelState.IsValid)
			{
				try
				{
					userService.Add(model);
					TempData["SuccessMessage"] = "User created successfully ✅";
					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					TempData["ErrorMessage"] = "Error: " + ex.Message;
				}
			}

			ViewBag.Roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
			return View(model);
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var user = userService.GetById(id);
			if (user == null)
				return NotFound();

			var model = new EditUserVM
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email,
				Role = user.Role
			};

			ViewBag.Roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();
			
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(int id, EditUserVM model)
		{
			if (id != model.Id)
				return BadRequest();

			if (ModelState.IsValid)
			{
				var updated = userService.Update(id, model);
				if (updated)
				{
					TempData["SuccessMessage"] = "User updated successfully ✏️";
					return RedirectToAction(nameof(Index));
				}
				else
				{
					TempData["ErrorMessage"] = "Update failed. Please try again.";
					return RedirectToAction(nameof(Index));
				}
			}

			ViewBag.Roles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>();

			return View(model);
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var deleted = userService.Delete(id);
			if (deleted)
			{
				TempData["SuccessMessage"] = "User deleted successfully 🗑️";
			}
			else
			{
				TempData["ErrorMessage"] = "Delete failed. Please try again.";
			}

			return RedirectToAction(nameof(Index));
		}

		// Remote Validation for Email
		public JsonResult IsEmailAvailable(string email, int id = 0)
        {
            var user = _userRepository.GetByEmail(email); 
            if (user != null && user.Id != id)
                return Json($"Email '{email}' is already in use.");

            return Json(true);
        }
    }
}
