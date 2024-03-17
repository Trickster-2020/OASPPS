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
    public class NotesController : ControllerBase
    {
        private readonly INoteService noteService;

        public NotesController(INoteService noteService)
        {
            this.noteService = noteService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> Get_Notes()
        {
            List<NoteDto> notesDtos = await noteService.Get_Note();
            return notesDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> Get_Notes(string id)
        {
            List<NoteDto> notesDtos = await noteService.Get_Note(id);
            return notesDtos;
        }
        [HttpGet("email/{email}")]
        public async Task<ActionResult<IEnumerable<NoteDto>>> Get_Notes_By_Email(string email)
        {
            List<NoteDto> notesDtos = await noteService.Get_Notes_By_Email(email);
            return notesDtos;
        }
        [HttpPost]
        public async Task<ActionResult<Note>> Post_teacher(NoteDto noteDto)
        {
            try
            {
                if (await noteService.Post_Note(noteDto))
                {
                    return Content($"{noteDto.Id} has been added.");
                }
                else
                {
                    return Content(noteDto.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(string id)
        {
            if (await noteService.Delete_Note(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateNote(NoteDto noteDto)
        {
            if (await noteService.Update_Note(noteDto))
            {
                return true;
            }
            return false;
        }
    }
}
