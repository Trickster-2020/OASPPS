using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ENotes.Repository
{
    public class EnrollledRepository : IEnrolledService
    {
        private readonly EnotesDbContext enotesDbContext;

        public EnrollledRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<bool> Delete_Enrolled(string Id)
        {
            Enrolled? data = await this.enotesDbContext.Enrolled.FindAsync(Id);
            if (data is not null)
            {
                this.enotesDbContext.Enrolled.Remove(data);
                await this.enotesDbContext.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<List<EnrolledDto>> Get_Enrolled()
        {
            List<Enrolled> datas = await enotesDbContext.Enrolled.ToListAsync();
            List<EnrolledDto> enrolledDtos = new List<EnrolledDto>();
            foreach (var data in datas)
            {
                EnrolledDto enrolledDto = new EnrolledDto();
                enrolledDto.Id = data.Id;    
                enrolledDto.Student_Id = data.Student_Id;
                Student? student= await this.enotesDbContext.Students.FindAsync(enrolledDto.Student_Id);
                enrolledDto.Student_Name = student.Name;

                enrolledDto.Semester_Id=data.Semester_Id;
                Semester? semester = await this.enotesDbContext.Semesters.FindAsync(enrolledDto.Semester_Id);
                enrolledDto.Semester_Name = semester.Name;
                enrolledDto.Enrolled_Date = data.Enrolled_Date;
                enrolledDtos.Add(enrolledDto);
            }
            return enrolledDtos;
        }

        public async Task<EnrolledDto> Get_Enrolled(string id)
        {
            Enrolled? enrolled = await enotesDbContext.Enrolled.FindAsync(id);
            EnrolledDto enrolledDto = new EnrolledDto();
            enrolledDto.Id = enrolled.Id;
            enrolledDto.Student_Id = enrolled.Student_Id;
            Student? student = await this.enotesDbContext.Students.FindAsync(enrolled.Student_Id);
            enrolledDto.Student_Name = student.Name;
            enrolledDto.Semester_Id=enrolled.Semester_Id;
            Semester? semester = await this.enotesDbContext.Semesters.FindAsync(enrolled.Semester_Id);
            enrolledDto.Semester_Name = semester.Name;
            enrolledDto.Enrolled_Date = enrolled.Enrolled_Date;
            return enrolledDto;
        }

        public async Task<bool> Post_Enrolled(EnrolledDto enrolled)
        {
            Enrolled new_enrolled = new Enrolled();
            new_enrolled.Id = enrolled.Id;
            new_enrolled.Student_Id= enrolled.Student_Id;
            new_enrolled.Semester_Id= enrolled.Semester_Id;
            new_enrolled.Enrolled_Date = enrolled.Enrolled_Date;
            enotesDbContext.Enrolled.Add(new_enrolled);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_Enrolled(EnrolledDto enrolled)
        {
            Enrolled old_enrolled = await this.enotesDbContext.Enrolled.FindAsync(enrolled.Id);
            if (old_enrolled is not null)
            {
                old_enrolled.Student_Id = enrolled.Student_Id;
                old_enrolled.Semester_Id = enrolled.Semester_Id;
                old_enrolled.Enrolled_Date = enrolled.Enrolled_Date;
                await enotesDbContext.SaveChangesAsync();
                return true;

            }
            return false;
        }
    }
}
