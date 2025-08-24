using ITIManagement.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ITIManagement.BLL.ViewModels.UserVM
{
	public class UserVM
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 3)]
		public string Name { get; set; } = default!;

		[Required]
		[EmailAddress]
		[Remote(action: "IsEmailAvailable", controller: "Users", AdditionalFields = "Id", ErrorMessage = "Email already exists.")]
		public string Email { get; set; } = default!;

		[Required]
		public UserRole Role { get; set; }
	}
}
