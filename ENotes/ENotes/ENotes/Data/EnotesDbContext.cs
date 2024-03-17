using ENotes.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Data
{
    public class EnotesDbContext:DbContext
    {
        public EnotesDbContext(DbContextOptions<EnotesDbContext> options): base(options){}
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Score>Scores { get; set; }
        public DbSet<Subject>Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teach> Teaches { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Enrolled> Enrolled { get; set; }
        public DbSet<QuizScore> QuizScores { get; set;}
    }
}
