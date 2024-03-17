using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.DTOs
{
    public class SubjectDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public string? Semester_Id { get; set; }
        public string Semester_Name { get; set; } = string.Empty;
    }
}
