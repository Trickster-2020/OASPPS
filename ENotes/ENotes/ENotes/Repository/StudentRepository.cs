using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class StudentRepository : IStudentService
    {
        private readonly EnotesDbContext enotesDbContext;

        public StudentRepository(EnotesDbContext enotesDbContext)
        {
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<bool> Delete_Student(string Id)
        {
            Student? student = await this.enotesDbContext.Students.FindAsync(Id);
            if (student == null)
            {
                return false;
            }
            else
            {
                this.enotesDbContext.Students.Remove(student);
                await this.enotesDbContext.SaveChangesAsync();
                return true;
            }
        }

        public async Task<StudentDto> Get_Student(string id)
        {
            Student? student = await this.enotesDbContext.Students.FindAsync(id);
            StudentDto studentdto = new StudentDto();
            studentdto.Id = id;
            studentdto.Name = student.Name;
            studentdto.Gender = student.Gender;
            studentdto.ContactNo = student.ContactNo;
            studentdto.Email = student.Email;
            studentdto.ImageURL = student.ImageURL;
            return studentdto;
        }

        public async Task<List<StudentDto>> Get_Students()
        {
            List<Student> students = await enotesDbContext.Students.ToListAsync();
            List<StudentDto> studentDtos = new List<StudentDto>();
            foreach (var studentdata in students)
            {
                StudentDto studentDto = new StudentDto();
                studentDto.Id = studentdata.Id;
                studentDto.Name = studentdata.Name;
                studentDto.Email = studentdata.Email;
                studentDto.Gender = studentdata.Gender;
                studentDto.ContactNo = studentdata.ContactNo;
                studentDto.ImageURL = studentdata.ImageURL;
                studentDtos.Add(studentDto);
            }
            return studentDtos;
        }

        public async Task<bool> Post_Student(StudentDto student)
        {
            Student new_student = new Student();
            new_student.Id = student.Id;
            new_student.Name = student.Name;
            new_student.Email = student.Email;
            new_student.Gender = student.Gender;
            new_student.ContactNo = student.ContactNo;
            new_student.ImageURL = student.ImageURL;

            enotesDbContext.Students.Add(new_student);
            await enotesDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update_Student(StudentDto student)
        {
            Student old_student = await this.enotesDbContext.Students.FindAsync(student.Id);
            if (old_student is not null)
            {
                old_student.Name = student.Name;
                old_student.Gender = student.Gender;
                old_student.Email = student.Email;
                old_student.ContactNo = student.ContactNo;
                old_student.ImageURL = student.ImageURL;
                await enotesDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
