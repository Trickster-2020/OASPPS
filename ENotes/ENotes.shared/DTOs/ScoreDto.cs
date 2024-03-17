using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.DTOs
{
    public class ScoreDto
    {
        
        public string Student_ID { get; set; }
        public string Student_Email { get; set; }
        public int Quiz { get; set; }
        public int Assignment01 { get; set; }
        public int FirstTerm_Exam { get; set; }
        public int Assignment02 { get; set; }
        public int Assignment03 { get; set; }
        public int SecondTerm_Exam { get; set; }
        public int Final_Grade { get; set; }
        public string? Class { get; set; }
    }
}
