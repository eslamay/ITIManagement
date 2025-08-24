using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace ITIManagement.BLL.ViewModels
{
    public class SessionVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course is required")]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public string? CourseName { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? Courses { get; set; }

    }
}






















