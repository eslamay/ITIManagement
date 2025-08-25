using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIManagement.BLL.ViewModels.Course
{
    public class CourseVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string? InstructorName { get; set; }

    }
}
