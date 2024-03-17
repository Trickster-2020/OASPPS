using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;


namespace ENotes.Repository
{
    public class SemesterRepository : ISemesterService
    {
        private readonly EnotesDbContext enotesDbContext;

        public SemesterRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }

        public async Task<bool> Delete_Semester(string id)
        {
            Semester? semester = new Semester();
            semester = await enotesDbContext.Semesters.FindAsync(id);
            if(semester is not null)
            {
                enotesDbContext.Semesters.Remove(semester);
                await enotesDbContext.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }

        public async Task<List<SemesterDto>> Get_Semester()
        {
            List<Semester> semesters = await enotesDbContext.Semesters.ToListAsync();
            List < SemesterDto > semesterDtos = new List<SemesterDto>();
            foreach (var semesterdata in semesters)
            { 
                SemesterDto semesterDto = new SemesterDto();
                semesterDto.Id = semesterdata.Id;
                semesterDto.Name = semesterdata.Name;
                semesterDtos.Add(semesterDto);
            }
            return semesterDtos;
        }

        public async Task<bool> Post_Semester(SemesterDto semester)
        {
            Semester? data = new Semester();      
            data.Id = semester.Id;
            data.Name = semester.Name;
            await enotesDbContext.Semesters.AddAsync(data);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_Semester(SemesterDto semester)
        {
            Semester old_semester = await this.enotesDbContext.Semesters.FindAsync(semester.Id);
            if(old_semester is not null)
            {
                old_semester.Id = semester.Id;
                old_semester.Name = semester.Name;
                await enotesDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
            
    }
}
