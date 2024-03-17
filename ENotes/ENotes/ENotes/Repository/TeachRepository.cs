using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class TeachRepository : ITeachService
    {
        private readonly EnotesDbContext enotesDbContext;

        public TeachRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<bool> Delete_Teach(string Id)
        {
            Teach? teach = await this.enotesDbContext.Teaches.FindAsync(Id);
            if (teach is not null)
            {
                this.enotesDbContext.Teaches.Remove(teach);
                await this.enotesDbContext.SaveChangesAsync();
                return true;              
            }
            return false;
        }
        public async Task<List<TeachDto>> Get_Teach()
        {
            List<Teach> teach = await enotesDbContext.Teaches.ToListAsync();
            List<TeachDto> teachDtos = new List<TeachDto>();
            foreach (var teachdata in teach)
            {
                TeachDto teachDto = new TeachDto();
                teachDto.Id = teachdata.Id;


                teachDto.Teacher_Id = teachdata.Teacher_Id;
                Teacher? teacher = await enotesDbContext.Teachers.FindAsync(teachDto.Teacher_Id);
                teachDto.Teacher_Name = teacher.Name;
                teachDto.Teacher_email = teacher.Email;

                teachDto.Subject_Id = teachdata.Subject_Id;
                Subject? sub = await enotesDbContext.Subjects.FindAsync(teachDto.Subject_Id);
                teachDto.Subject_Name = sub.Name;
                
                teachDtos.Add(teachDto);
            }
            return teachDtos;
        }

        public async Task<TeachDto> Get_Teach(string id)
        {
            Teach? teach = await this.enotesDbContext.Teaches.FindAsync(id);
            TeachDto teachdto = new TeachDto();
            teachdto.Id = id;

            teachdto.Teacher_Id = teach.Teacher_Id;
            Teacher? teacher = new Teacher();
            teacher = await enotesDbContext.Teachers.FindAsync(teach.Teacher_Id);
            teachdto.Teacher_Name = teacher.Name;

            teachdto.Subject_Id = teach.Subject_Id;
            Subject? sub = new Subject();
            sub = await enotesDbContext.Subjects.FindAsync(teach.Subject_Id);
            teachdto.Subject_Name = sub.Name;
            
            return teachdto;
        }
        public async Task<bool> Post_Teach(TeachDto teach)
        {
            Teach? check = new Teach();            
            check.Id = teach.Id;
            check.Teacher_Id = teach.Teacher_Id;
            check.Subject_Id = teach.Subject_Id;

            enotesDbContext.Teaches.Add(check);
            await enotesDbContext.SaveChangesAsync();
            return false;
            
        }

        public async Task<bool> Update_Teach(TeachDto teach)
        {
            Teach old_teach = await this.enotesDbContext.Teaches.FindAsync(teach.Id);
            if (old_teach is not null)
            {
                old_teach.Id = teach.Id;
                old_teach.Teacher_Id = teach.Teacher_Id;
                old_teach.Subject_Id = teach.Subject_Id;
                
               
                await enotesDbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
