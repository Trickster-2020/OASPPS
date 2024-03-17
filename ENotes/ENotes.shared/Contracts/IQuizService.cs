using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface IQuizService
    {
        public Task<List<QuizDto>> Get_Quiz();
        public Task<List<QuizDto>> Get_Quiz(string id);
        public Task<bool> Post_Quiz(QuizDto quiz);
        public Task<bool> Delete_Quiz(String Id);
        public Task<bool> Update_Quiz(QuizDto quiz);
    }
}
