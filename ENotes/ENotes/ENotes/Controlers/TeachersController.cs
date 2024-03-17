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
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeacherDto>>> Get_teachers()
        {
            List<TeacherDto> teachersDtos = await teacherService.Get_Teachers();
            return teachersDtos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> Get_teachers(string id)
        {
            TeacherDto? teachersDtos = await teacherService.Get_Teacher(id);
            return teachersDtos;
        }
        [HttpPost]
        public async Task<ActionResult<Teacher>> Post_teacher(TeacherDto teacherDto)
        {
            try
            {
                if (await teacherService.Post_Teacher(teacherDto))
                {
                    return Content($"{teacherDto.Id} has been added.");
                }
                else
                {
                    return Content(teacherDto.Id + " updated to database.");
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
            if (await teacherService.Delete_Teacher(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateTeacher(TeacherDto teacherDto)
        {
            if(await teacherService.Update_Teacher(teacherDto))
            {
                return true;
            }
            return false;
        }
    }
}
