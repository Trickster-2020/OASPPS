
using ENotes.Client.Services;
using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENotes.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachesController : ControllerBase
    {
        private readonly ITeachService teachService;

        public TeachesController(ITeachService teachService)
        {
            this.teachService = teachService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeachDto>>> Get_teaches()
        {
            List<TeachDto> teacheDtos = await teachService.Get_Teach();
            return teacheDtos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TeachDto>> Get_teaches(string id)
        {
            TeachDto? teachDtos = await teachService.Get_Teach(id);
            return teachDtos;
        }
        [HttpPost]
        public async Task<ActionResult<Teach>> Post_teach(TeachDto teachDto)
        {
            try
            {
                if (await teachService.Post_Teach(teachDto))
                {
                    return Content($"{teachDto.Id} has been added.");
                }
                else
                {
                    return Content(teachDto.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeach(string id)
        {
            if (await teachService.Delete_Teach(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateTeacher(TeachDto teachDto)
        {
            if (await teachService.Update_Teach(teachDto))
            {
                return true;
            }
            return false;
        }
    }
}
