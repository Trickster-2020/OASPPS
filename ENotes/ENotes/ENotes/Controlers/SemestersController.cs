using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using ENotes.Client.Services;

namespace ENotes.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        private readonly ISemesterService _semesterservices;

        public SemestersController(ISemesterService semester)
        {          
            this._semesterservices = semester;
        }

        //GET: api/Semesters
       [HttpGet]
        public async Task<ActionResult<IEnumerable<SemesterDto>>> GetSemesters()
        {
            List<SemesterDto> semestersDtos = await _semesterservices.Get_Semester();
            return semestersDtos;
        }

        [HttpPost]
        public async Task<ActionResult<Semester>> PostSemester(SemesterDto semester)
        {
            try
            {
                if (await _semesterservices.Post_Semester(semester))
                {
                    return Content($"{semester.Id} has been added.");
                }
                else
                {
                    return Content(semester.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }

        // DELETE: api/Semesters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSemester(string id)
        { 
            if (await _semesterservices.Delete_Semester(id))
            {
                return Content($"Data with Id:{id} has been deleted.");
            }
            else
            {
                return Content($"Error while deleting data with Id:{id}.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateSemester(SemesterDto semesterDto)
        {
            if (await _semesterservices.Update_Semester(semesterDto))
            {
                return true;
            }
            return false;
        }

    }
}
