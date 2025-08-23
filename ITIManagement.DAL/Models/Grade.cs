using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITIManagement.DAL.Models
{
	public class Grade
	{
		public int Id { get; set; }

		[Required]
		[Range(0, 100)]
		public int Value { get; set; }

		[Required]
		[ForeignKey("Session")]
		public int? SessionId { get; set; }
		public Session? Session { get; set; }

		[Required]
		[ForeignKey("Trainee")]
		public int TraineeId { get; set; }
		public User? Trainee { get; set; }
	}
}
