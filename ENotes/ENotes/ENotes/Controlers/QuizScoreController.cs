using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENotes.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizScoreController : ControllerBase
    {
        private readonly IQuizScoreService quizScoreService;

        public QuizScoreController(IQuizScoreService quizScoreService)
        {
            this.quizScoreService = quizScoreService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizScoreDto>>> Get_QuizScore()
        {
            List<QuizScoreDto> quizScoreDtos = await quizScoreService.Get_QuizScore();
            return quizScoreDtos;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizScoreDto>> Get_QuizScore(string id)
        {
            QuizScoreDto? quizScoreDto = await quizScoreService.Get_QuizScore(id);
            return quizScoreDto;
        }
        [HttpPost]
        public async Task<ActionResult<QuizScore>> Post_QuizScore(QuizScoreDto quizScoreDto)
        {
            try
            {
                if (await quizScoreService.Post_QuizScore(quizScoreDto))
                {
                    return Content($"{quizScoreDto.Id} has been added.");
                }
                else
                {
                    return Content(quizScoreDto.Id + " updated to database.");
                }
                throw new Exception("Error while adding/updating");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuizScore(string id)
        {
            if (await quizScoreService.Delete_QuizScore(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateQuizScore(QuizScoreDto quizScoreDto)
        {
            if (await quizScoreService.Update_QuizScore(quizScoreDto))
            {
                return true;
            }
            return false;
        }
    }
}
