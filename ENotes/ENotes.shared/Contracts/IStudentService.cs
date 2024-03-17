using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface IStudentService
    { 
        public Task<List<StudentDto>> Get_Students();
        public Task<StudentDto> Get_Student(string id);
        public Task<bool> Post_Student(StudentDto student);
        public Task<bool> Delete_Student(String Id);
        public Task<bool> Update_Student(StudentDto student);
    }
}
