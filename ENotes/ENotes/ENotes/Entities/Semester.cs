using System.ComponentModel.DataAnnotations;

namespace ENotes.Entities
{
    public class Semester
    {
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
