using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface ITeacherService
    {
        public Task<List<TeacherDto>> Get_Teachers();
        public Task<TeacherDto> Get_Teacher(string id);
        public Task<bool> Post_Teacher(TeacherDto teacher);
        public Task<bool> Delete_Teacher(String Id);
        public Task<bool> Update_Teacher(TeacherDto teacher);
    }
}
