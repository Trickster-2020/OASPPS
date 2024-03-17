using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.DTOs
{
    public class QuizDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Answer { get; set; }
        public string Subject_Id { get; set; }
        public string Subject_Name { get; set; }
    }
}
