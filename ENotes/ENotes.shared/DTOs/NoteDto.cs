using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.DTOs
{
    public class NoteDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int Download_Count { get; set; }
        public string Teacher_Id { get; set; }
        public string Teacher_Name { get; set; }
        public string Subject_Id { get; set; }
        public string Subject_Name { get; set; }
        public string Note_Data { get; set; }
    }
}
