using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.DTOs
{
    public class TeachDto
    {
        public string Id { get; set; }
        public string Teacher_Id { get; set; }
        public string Teacher_Name { get; set; }
        public string Teacher_email { get; set; }
        public string Subject_Id { get; set; }
        public string Subject_Name { get; set; }
    }
}
