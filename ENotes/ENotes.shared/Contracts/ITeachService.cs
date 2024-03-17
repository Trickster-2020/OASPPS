using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENotes.shared.Contracts
{
    public interface ITeachService
    {
        public Task<List<TeachDto>> Get_Teach();
        public Task<TeachDto> Get_Teach(string id);
        public Task<bool> Post_Teach(TeachDto teach);
        public Task<bool> Delete_Teach(String Id);
        public Task<bool> Update_Teach(TeachDto teach);

    }
}
