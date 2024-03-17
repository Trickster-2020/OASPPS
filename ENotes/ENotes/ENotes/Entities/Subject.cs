using System.ComponentModel.DataAnnotations;

namespace ENotes.Entities
{
    public class Subject
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Semester_Id { get; set; }
    }
}
