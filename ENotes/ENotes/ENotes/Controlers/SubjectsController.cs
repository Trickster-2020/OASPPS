
using Microsoft.AspNetCore.Mvc;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using ENotes.Entities;

namespace ENotes.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly ISubjectService subjectService;
        public SubjectsController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubjects()
        {
            List<SubjectDto> subjectsDtos = await subjectService.Get_Subject();
            return subjectsDtos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetSubject(string id)
        {
            List<SubjectDto> subjectsDtos = await subjectService.GetSubjectBySemester(id);
            return subjectsDtos;
        }

        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(SubjectDto subjectDto)
        {
            try
            {
                if (await subjectService.Post_Subject(subjectDto))
                {
                    return Content($"{subjectDto.Id} has been added.");
                }
                else
                {
                    return Content(subjectDto.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubject(string id)
        {
            if (await subjectService.Delete_Subject(id))
            {
                return Content(id+" deleted successfully.");
            }
            else
            {
                return Content("Could not delete "+id);
            }
           
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateSubject(SubjectDto subjectDto)
        {
            if (await subjectService.Update_Subject(subjectDto))
            {
                return true;
            }
            return false;
        }
    }
}
