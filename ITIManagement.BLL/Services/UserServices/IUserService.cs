using ITIManagement.BLL.Pagination;
using ITIManagement.BLL.ViewModels.UserVM;
using ITIManagement.DAL.Models;

namespace ITIManagement.BLL.Services.UserServices
{
	public interface IUserService
	{
		UserDetailsVM? GetById(int id);
		PageResult<UserVM> GetAllPage(int page, int pageSize, string? serachName = null,UserRole? role = null);
		void Add(CreateUserVM createUserVM);
		bool Update(int id, EditUserVM editUserVM);
		bool Delete(int id);
	}
}
