using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using ENotes.Entities;
using ENotes.Data;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class SubjectRepository : ISubjectService
    {
        private readonly EnotesDbContext enotesDbContext;

        public SubjectRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }

        public async Task<bool> Delete_Subject(string Id)
        {
            Subject? subject = await this.enotesDbContext.Subjects.FindAsync(Id);
            if(subject == null)
            {
                return false;
            }
            else
            {
                this.enotesDbContext.Subjects.Remove(subject);
                await this.enotesDbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<List<SubjectDto>> GetSubjectBySemester(string SemesterId)
        {
            List<Subject> subjects = await enotesDbContext.Subjects.Where(s=> s.Semester_Id == SemesterId).ToListAsync();
            List<SubjectDto> subjectDtos = new List<SubjectDto>();
            foreach (var subjectdata in subjects)
            {
                SubjectDto subjectDto = new SubjectDto();
                subjectDto.Id = subjectdata.Id;
                subjectDto.Name = subjectdata.Name;

                subjectDto.Semester_Id = subjectdata.Semester_Id;
                Semester? sem = await enotesDbContext.Semesters.FindAsync(subjectdata.Semester_Id);
                subjectDto.Semester_Name = sem.Name;

                subjectDtos.Add(subjectDto);
            }
            return subjectDtos;
        }

        public async Task<List<SubjectDto>> Get_Subject()
        {
            List<Subject> subjects = await enotesDbContext.Subjects.ToListAsync();
            List<SubjectDto> subjectDtos = new List<SubjectDto>();
            foreach (var subjectdata in subjects)
            {
                SubjectDto subjectDto = new SubjectDto();
                subjectDto.Id= subjectdata.Id;
                subjectDto.Name= subjectdata.Name;

                subjectDto.Semester_Id= subjectdata.Semester_Id;
                Semester? sem = await enotesDbContext.Semesters.FindAsync(subjectdata.Semester_Id);
                subjectDto.Semester_Name = sem.Name;

                subjectDtos.Add(subjectDto);
            }
            return subjectDtos;
        }


        public async Task<bool> Post_Subject(SubjectDto subject)
        {
            
            Subject new_subject = new Subject();
            new_subject.Id = subject.Id;
            new_subject.Name = subject.Name;
            new_subject.Semester_Id = subject.Semester_Id;
            enotesDbContext.Subjects.Add(new_subject);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_Subject(SubjectDto subject)
        {
            Subject old_subject = await this.enotesDbContext.Subjects.FindAsync(subject.Id);
            if (old_subject is not null)
            {
                old_subject.Id = subject.Id;
                old_subject.Name = subject.Name;
                old_subject.Semester_Id = subject.Semester_Id;
                await enotesDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
