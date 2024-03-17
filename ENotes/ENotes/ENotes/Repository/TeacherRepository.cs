using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class TeacherRepository : ITeacherService
    {
        private readonly EnotesDbContext enotesDbContext;

        public TeacherRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<bool> Delete_Teacher(string Id)
        {
            Teacher? teacher = await this.enotesDbContext.Teachers.FindAsync(Id);
            if (teacher == null)
            {
                return false;
            }
            else
            {
                this.enotesDbContext.Teachers.Remove(teacher);
                await this.enotesDbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<TeacherDto> Get_Teacher(string id)
        {
            Teacher? teacher = await this.enotesDbContext.Teachers.FindAsync(id);
            TeacherDto teacherdto = new TeacherDto();
            teacherdto.Id = id;
            teacherdto.Name = teacher.Name;
            teacherdto.Gender = teacher.Gender;
            teacherdto.ContactNo = teacher.ContactNo;
            teacherdto.Email = teacher.Email;
            teacherdto.ImageURL = teacher.ImageURL;
            return teacherdto;
        }

        public async Task<List<TeacherDto>> Get_Teachers()
        {
            List<Entities.Teacher> teachers = await enotesDbContext.Teachers.ToListAsync();
            List<TeacherDto> teacherDtos = new List<TeacherDto>();
            foreach (var teacherdata in teachers)
            {
                TeacherDto teacherDto = new TeacherDto();
                teacherDto.Id = teacherdata.Id;
                teacherDto.Name = teacherdata.Name;
                teacherDto.Email = teacherdata.Email;
                teacherDto.Gender = teacherdata.Gender;
                teacherDto.ContactNo = teacherdata.ContactNo;
                teacherDto.ImageURL = teacherdata.ImageURL;
                teacherDtos.Add(teacherDto);
            }
            return teacherDtos;
            
        }

        public async Task<bool> Post_Teacher(TeacherDto teacher)
        {
            Entities.Teacher new_teacher = new Teacher();
            new_teacher.Id = teacher.Id;
            new_teacher.Name = teacher.Name;
            new_teacher.Email = teacher.Email;
            new_teacher.Gender = teacher.Gender;
            new_teacher.ContactNo = teacher.ContactNo;
            new_teacher.ImageURL = teacher.ImageURL;

            enotesDbContext.Teachers.Add(new_teacher);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_Teacher(TeacherDto teacher)
        {
            Teacher old_teacher = await this.enotesDbContext.Teachers.FindAsync(teacher.Id);
            if (old_teacher == null)
            {
                return false;
            }
            else
            {
                old_teacher.Name = teacher.Name;
                old_teacher.Gender = teacher.Gender;
                old_teacher.Email = teacher.Email;
                old_teacher.ContactNo = teacher.ContactNo;
                old_teacher.ImageURL= teacher.ImageURL;
                await enotesDbContext.SaveChangesAsync();
                return true;
            }

        }
    }
}
