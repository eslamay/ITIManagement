using ITIManagement.BLL.Validations;
using ITIManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITIManagement.BLL.ViewModels.UserVM
{
	public class CreateUserVM
	{

		[Required]
		[StringLength(50, MinimumLength = 3)]
		[NoNumbers]
		public string Name { get; set; } = default!;

		[Required]
		[EmailAddress]
		[Remote(action: "IsEmailAvailable", controller: "Users", ErrorMessage = "Email already exists.")]
		public string Email { get; set; } = default!;

		[Required]
		public UserRole Role { get; set; }

	}
}
