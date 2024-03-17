using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface ISemesterService
    {
        Task<List<SemesterDto>> Get_Semester();
        public Task<bool> Post_Semester(SemesterDto semester);
        public Task<bool> Update_Semester(SemesterDto semester);
        Task<bool> Delete_Semester(string id);
        
    }
}
