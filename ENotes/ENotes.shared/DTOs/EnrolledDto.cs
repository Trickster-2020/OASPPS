using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.DTOs
{
    public class EnrolledDto
    {
        public string Id { get; set; }
        public string Student_Id { get; set; }
        public string Student_Name { get; set; }
        public string Semester_Id { get; set; }
        public string Semester_Name { get; set; }
        public DateTime Enrolled_Date { get; set; }
    }
}
