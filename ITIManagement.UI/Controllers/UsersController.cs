using ITIManagement.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITIManagement.UI.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Create() => View();

        // Remote Validation for Email
        public JsonResult IsEmailAvailable(string email, int id = 0)
        {
            var user = _userRepository.GetByName(email); 
            if (user != null && user.Id != id)
                return Json($"Email '{email}' is already in use.");

            return Json(true);
        }
    }
}
