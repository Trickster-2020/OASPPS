using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface ISubjectService
    {
        public Task<List<SubjectDto>> Get_Subject();
        public Task<bool> Post_Subject(SubjectDto subject);
        public Task<bool> Update_Subject(SubjectDto subject);
        public Task<bool> Delete_Subject(String Id);

        public Task<List<SubjectDto>> GetSubjectBySemester(String SemesterId);
        
    }
}
