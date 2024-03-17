using ENotes.Data;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class RoleRepository : IRoleService
    {
        private readonly ApplicationDbContext dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<bool> Delete_Role(string id)
        {
            var role = await dbContext.Roles.FindAsync(id);
            
            try
            {
                if(role is not null)
                {
                    dbContext.Roles.Remove(role);
                    await dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
                
            }
            catch (Exception)
            {

                return false;
            }
        }

        public  async Task<List<RoleDto>> Get_Roles()
        {
            List<IdentityRole> identityRoles= await dbContext.Roles.ToListAsync();
            List<RoleDto> roleDtos = new List<RoleDto>();
            foreach (var role in identityRoles)
            {
                RoleDto roleDto = new RoleDto();
                roleDto.Id = role.Id;
                roleDto.Name = role.Name;
                roleDtos.Add(roleDto);
            }
            return roleDtos;
        }

        public async Task<bool> Post_Role(RoleDto role)
        {         
            var data = await dbContext.Roles.FindAsync(role.Id);
            if(data is not null)
            {
                data.Id = role.Id;
                data.Name = role.Name;
                await dbContext.SaveChangesAsync();
                return true;
            }
            else if (data is null)
            {
                IdentityRole identityRole = new();
                identityRole.Id = role.Id;
                identityRole.Name = role.Name;
                dbContext.Roles.Add(identityRole);
                await dbContext.SaveChangesAsync();
                return true;   
            }
            return false;
        }

       
    }
}
