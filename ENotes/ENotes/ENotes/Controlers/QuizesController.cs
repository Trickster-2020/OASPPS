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
    public class QuizesController : ControllerBase
    {
        private readonly IQuizService quizService;

        public QuizesController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> Get_Quizes()
        {
            List<QuizDto> quizesDtos = await quizService.Get_Quiz();
            return quizesDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<QuizDto>>> Get_Quiz(string id)
        {
            List<QuizDto> quizesDtos = await quizService.Get_Quiz(id);
            return quizesDtos;
        }
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(QuizDto quizDto)
        {
            try
            {
                if (await quizService.Post_Quiz(quizDto))
                {
                    return Content($"{quizDto.Id} has been added.");
                }
                else
                {
                    return Content(quizDto.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuiz(string id)
        {
            if (await quizService.Delete_Quiz(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateQuiz(QuizDto quizDto)
        {
            if (await quizService.Update_Quiz(quizDto))
            {
                return true;
            }
            return false;
        }
    }
}
