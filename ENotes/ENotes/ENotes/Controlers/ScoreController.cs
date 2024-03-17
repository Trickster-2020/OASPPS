using ENotes.Client.Services;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ENotes.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreServices scoreservices;

        public ScoreController(IScoreServices scoreservices)
        {
            this.scoreservices = scoreservices;
        }
        [HttpGet]
        public async Task<ActionResult<List<ScoreDto>>> Get_Scores()
        {
            List<ScoreDto> scores = await scoreservices.Get_Scores();
            return scores;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ScoreDto>> Get_scores(string id)
        {
            ScoreDto? scoresDto = await scoreservices.Get_Score(id);
            return scoresDto;
        }
        [HttpPost]
        public async Task<ActionResult<String>> post_Score(ScoreDto score)
        {
            if(await scoreservices.Post_Score(score)) {
                return "Data has been added.";
            }
            else
            {
                return "Problem occured while adding data.";
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteScore(string id)
        {
            if (await scoreservices.Delete_Score(id))
            {
                return Content(id + " deleted successfully.");
            }
            else
            {
                return Content("Could not delete " + id);
            }

        }
        [HttpPut]
        public async Task<ActionResult<bool>> UpdateScore(ScoreDto scoreDto)
        {
            if (await scoreservices.Update_Score(scoreDto))
            {
                return true;
            }
            return false;
        }

    }
}
