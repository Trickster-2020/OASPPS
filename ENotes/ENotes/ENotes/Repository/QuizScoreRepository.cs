using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENotes.Repository
{
    public class QuizScoreRepository : IQuizScoreService
    {
        private readonly EnotesDbContext enotesDbContext;

        public QuizScoreRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<bool> Delete_QuizScore(string Id)
        {
            QuizScore? data = await this.enotesDbContext.QuizScores.FindAsync(Id);
            if (data is not null)
            {
                this.enotesDbContext.QuizScores.Remove(data);
                await this.enotesDbContext.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<List<QuizScoreDto>> Get_QuizScore()
        {
            List<QuizScore> datas = await enotesDbContext.QuizScores.ToListAsync();
            List<QuizScoreDto> quizScoreDtos = new List<QuizScoreDto>();
            foreach (var data in datas)
            {
                QuizScoreDto quizScoreDto = new QuizScoreDto();
                quizScoreDto.Id = data.Id;
                quizScoreDto.Quiz_Id=data.Quiz_Id;
                Quiz? quiz = await this.enotesDbContext.Quizes.FindAsync(quizScoreDto.Quiz_Id);
                quizScoreDto.Quiz_Title = quiz.Title;
                quizScoreDto.Student_Id = data.Student_Id;
                Student? student = await this.enotesDbContext.Students.FindAsync(quizScoreDto.Student_Id);
                quizScoreDto.Student_Name = student.Name;

                quizScoreDto.Score = data.Score;
                quizScoreDtos.Add(quizScoreDto);
            }
            return quizScoreDtos;
        }

        public async Task<QuizScoreDto> Get_QuizScore(string id)
        {
            QuizScore? quizScore = await enotesDbContext.QuizScores.FindAsync(id);
            QuizScoreDto quizScoreDto = new QuizScoreDto();
            quizScoreDto.Id = quizScore.Id;
            quizScoreDto.Quiz_Id=quizScore.Quiz_Id;
            Quiz? quiz = await this.enotesDbContext.Quizes.FindAsync(quizScoreDto.Quiz_Id);
            quizScoreDto.Quiz_Title = quiz.Title;
            quizScoreDto.Student_Id = quizScore.Student_Id;
            Student? student = await this.enotesDbContext.Students.FindAsync(quizScoreDto.Student_Id);
            quizScoreDto.Student_Name = student.Name;
            quizScoreDto.Score = quizScore.Score;
            return quizScoreDto;
        }

        public async Task<bool> Post_QuizScore(QuizScoreDto quizScore)
        {
            QuizScore new_quizScore = new QuizScore();
            new_quizScore.Id = quizScore.Id;
            new_quizScore.Student_Id = quizScore.Student_Id;
            new_quizScore.Quiz_Id = quizScore.Quiz_Id;
            new_quizScore.Score = quizScore.Score;
            enotesDbContext.QuizScores.Add(new_quizScore);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_QuizScore(QuizScoreDto quizScore)
        {
            QuizScore old_quizScore = await this.enotesDbContext.QuizScores.FindAsync(quizScore.Id);
            if (old_quizScore is not null)
            {
                old_quizScore.Id = quizScore.Id;
                old_quizScore.Student_Id = quizScore.Student_Id;
                old_quizScore.Quiz_Id = quizScore.Quiz_Id;
                old_quizScore.Score = quizScore.Score;
                await enotesDbContext.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
