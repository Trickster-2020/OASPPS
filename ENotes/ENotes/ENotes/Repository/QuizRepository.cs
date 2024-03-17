
using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class QuizRepository : IQuizService
    {
        private readonly EnotesDbContext enotesDbContext;

        public QuizRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<bool> Delete_Quiz(string Id)
        {
            Quiz? quiz = await this.enotesDbContext.Quizes.FindAsync(Id);
            if (quiz is not null)
            {
                this.enotesDbContext.Quizes.Remove(quiz);
                await this.enotesDbContext.SaveChangesAsync();
                return true;
            }
           return false;
        }

        public async Task<List<QuizDto>> Get_Quiz()
        {
            List<Quiz> quizes = await enotesDbContext.Quizes.ToListAsync();
            List<QuizDto> quizDtos = new List<QuizDto>();
            foreach (var quizdata in quizes)
            {
                QuizDto quizDto = new QuizDto();
                quizDto.Id = quizdata.Id;
                quizDto.Title = quizdata.Title;
                quizDto.Description = quizdata.Description;
                quizDto.Option1 = quizdata.Option1;
                quizDto.Option2 = quizdata.Option2;
                quizDto.Option3 = quizdata.Option3;
                quizDto.Option4 = quizdata.Option4;
                quizDto.Answer = quizdata.Answer;
                quizDto.Subject_Id = quizdata.Subject_Id;
                Subject subject=await enotesDbContext.Subjects.FindAsync(quizdata.Subject_Id); 
                quizDto.Subject_Name = subject.Name;
                quizDtos.Add(quizDto);
            }
            return quizDtos;
        }

        public async Task<List<QuizDto>> Get_Quiz(string id)
        {
            List<Quiz>? quizes = await enotesDbContext.Quizes.Where(s => s.Subject_Id == id).ToListAsync();
            List<QuizDto> quizDtos = new List<QuizDto>();
            foreach (var quizdata in quizes)
            {
                QuizDto quizDto = new QuizDto();
                quizDto.Id = quizdata.Id;
                quizDto.Title = quizdata.Title;
                quizDto.Description = quizdata.Description;
                quizDto.Option1 = quizdata.Option1;
                quizDto.Option2 = quizdata.Option2;
                quizDto.Option3 = quizdata.Option3;
                quizDto.Option4 = quizdata.Option4;
                quizDto.Answer = quizdata.Answer;
                quizDto.Subject_Id = quizdata.Subject_Id;
                Subject subject = await enotesDbContext.Subjects.FindAsync(quizdata.Subject_Id);
                quizDto.Subject_Name = subject.Name;
                quizDtos.Add(quizDto);
            }
            return quizDtos;
        }

        public async Task<bool> Post_Quiz(QuizDto quiz)
        {
            Quiz new_quiz = new Quiz();

            new_quiz.Id = quiz.Id;
            new_quiz.Title = quiz.Title;
            new_quiz.Description = quiz.Description;
            new_quiz.Option1 = quiz.Option1;
            new_quiz.Option2 = quiz.Option2;
            new_quiz.Option3 = quiz.Option3;
            new_quiz.Option4 = quiz.Option4;
            new_quiz.Answer = quiz.Answer;    
            new_quiz.Subject_Id = quiz.Subject_Id;
            enotesDbContext.Quizes.Add(new_quiz);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_Quiz(QuizDto quiz)
        {
            Quiz old_quiz = await this.enotesDbContext.Quizes.FindAsync(quiz.Id);
            if (old_quiz is not null)
            {
                old_quiz.Title = quiz.Title;
                old_quiz.Description = quiz.Description;
                old_quiz.Option1 = quiz.Option1;
                old_quiz.Option2 = quiz.Option2;
                old_quiz.Option3 = quiz.Option3;
                old_quiz.Option4 = quiz.Option4;
                old_quiz.Answer = quiz.Answer;
                old_quiz.Subject_Id = quiz.Subject_Id;
                await enotesDbContext.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
