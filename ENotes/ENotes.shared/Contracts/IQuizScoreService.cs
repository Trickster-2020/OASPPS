using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface IQuizScoreService
    {
        public Task<List<QuizScoreDto>> Get_QuizScore();
        public Task<QuizScoreDto> Get_QuizScore(string id);
        public Task<bool> Post_QuizScore(QuizScoreDto quizScore);
        public Task<bool> Delete_QuizScore(String Id);
        public Task<bool> Update_QuizScore(QuizScoreDto quizScore);
    }
}
