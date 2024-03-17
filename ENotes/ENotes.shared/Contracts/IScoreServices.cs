using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface IScoreServices
    {
        public Task<List<ScoreDto>> Get_Scores();
        public Task<ScoreDto> Get_Score(string id);
        public Task<bool> Post_Score(ScoreDto score);
        public Task<bool> Delete_Score(String Id);
        public Task<bool> Update_Score(ScoreDto score);
    }
}
