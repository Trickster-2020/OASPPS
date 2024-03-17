using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.DTOs
{
    public class QuizScoreDto
    {
        public string Id { get; set; }
        public string Student_Id { get; set; }
        public string Student_Name { get; set; }
        public string Quiz_Id { get; set; }
        public string Quiz_Title { get; set; }
        public string Score { get; set; }
    }
}
