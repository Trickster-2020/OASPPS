using ENotes.shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENotes.shared.Contracts
{
    public interface IRoleService
    {
        Task<List<RoleDto>> Get_Roles();
        public Task<bool> Post_Role(RoleDto role);
        Task<bool> Delete_Role(string id);
        

    }
}
