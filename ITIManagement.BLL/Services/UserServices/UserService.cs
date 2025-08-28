using ITIManagement.BLL.Pagination;
using ITIManagement.BLL.ViewModels.UserVM;
using ITIManagement.DAL.Interfaces;
using ITIManagement.DAL.Models;

namespace ITIManagement.BLL.Services.UserServices
{
	public class UserService : IUserService
	{
		private readonly IUserRepository userRepository;

		public UserService(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}
		public PageResult<UserVM> GetAllPage(int page, int pageSize, string? serachName = null, UserRole? role = null)
		{
			var items = userRepository.GetAll(serachName!, role,page, pageSize).Select(
				u => new UserVM
				{
					Id = u.Id,
					Name = u.Name,
					Email = u.Email,
					Role = u.Role
				}).ToList();

			var totalCount = userRepository.GetCount(serachName,role);

			return new PageResult<UserVM>
			{
				Items = items,
				TotalCount = totalCount,
				Page = page,
				PageSize = pageSize
			};
		}

		public UserDetailsVM? GetById(int id)
		{
			var user = userRepository.GetById(id);
			if (user == null)
			{
				return null;
			}

			return new UserDetailsVM
			{
				Id = user.Id,
				Name = user.Name,
				Email = user.Email,
				Role = user.Role,
				Courses = user.Courses?.Select(c => new Course
				{
					Id = c.Id,
					Name = c.Name,
					Category = c.Category
				}).ToList() ?? new List<Course>(),
				Grades = user.Grades?.Select(g => new Grade
				{
					Id = g.Id,
					Value = g.Value,
				}).ToList() ?? new List<Grade>()
			};
		}
		public void Add(CreateUserVM createUserVM)
		{
			if (userRepository.GetByEmail(createUserVM.Email) != null)
				return;

			var user = new User
			{
                Name = createUserVM.Name,
				Email = createUserVM.Email,
				Role = createUserVM.Role
            };

			userRepository.Add(user);
		}
		public bool Update(int id, EditUserVM editUserVM)
		{
			var user = userRepository.GetById(id);

			var existing = userRepository.GetByEmail(editUserVM.Email);
			if (existing != null && existing.Id != id)
				return false;

			if (user == null)
			{
				return false;
			}

			user.Name = editUserVM.Name;
			user.Email = editUserVM.Email;
			user.Role = editUserVM.Role;

			userRepository.Update(user);
			return true;
		}
		public bool Delete(int id)
		{
			userRepository.Delete(id);
			return true;
		}	
	}
}
