using ENotes.Client.Services;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENotes.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentDto>>> Get_students()
        {
            List<StudentDto> studentsDtos = await studentService.Get_Students();
            return studentsDtos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentDto>> Get_student(string id)
        {
            StudentDto? studentsDtos = await studentService.Get_Student(id);
            return studentsDtos;
        }
        [HttpPost]
        public async Task<ActionResult<Student>> Post_student(StudentDto studentDto)
        {
            try
            {
                if (await studentService.Post_Student(studentDto))
                {
                    return Content($"{studentDto.Id} has been added.");
                }
                else
                {
                    return Content(studentDto.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(string id)
        {
            if (await studentService.Delete_Student(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateStudent(StudentDto studentDto)
        {
            if (await studentService.Update_Student(studentDto))
            {
                return true;
            }
            return false;
        }
    }
}
