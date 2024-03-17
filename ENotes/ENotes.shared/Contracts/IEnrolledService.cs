using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface IEnrolledService
    {
        public Task<List<EnrolledDto>> Get_Enrolled();
        public Task<EnrolledDto> Get_Enrolled(string id);
        public Task<bool> Post_Enrolled(EnrolledDto enrolled);
        public Task<bool> Delete_Enrolled(String Id);
        public Task<bool> Update_Enrolled(EnrolledDto enrolled);
    }
}
