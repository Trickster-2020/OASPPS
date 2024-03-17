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
    public class EnrolledController : ControllerBase
    {
        private readonly IEnrolledService enrolledService;

        public EnrolledController(IEnrolledService enrolledService)
        {
            this.enrolledService = enrolledService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrolledDto>>> Get_Enrolled()
        {
            List<EnrolledDto> enrolledDtos = await enrolledService.Get_Enrolled();
            return enrolledDtos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrolledDto>> Get_notes(string id)
        {
            EnrolledDto? enrolledDtos = await enrolledService.Get_Enrolled(id);
            return enrolledDtos;
        }
        [HttpPost]
        public async Task<ActionResult<Enrolled>> Post_Enrolled(EnrolledDto enrolledDto)
        {
            try
            {
                if (await enrolledService.Post_Enrolled(enrolledDto))
                {
                    return Content($"{enrolledDto.Id} has been added.");
                }
                else
                {
                    return Content(enrolledDto.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrolled(string id)
        {
            if (await enrolledService.Delete_Enrolled(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateEnrolled(EnrolledDto enrolledDto)
        {
            if (await enrolledService.Update_Enrolled(enrolledDto))
            {
                return true;
            }
            return false;
        }
    }
}
