using ENotes.Client.Services;
using ENotes.Data;
using ENotes.Entities;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ENotes.Repository
{
    public class RegisterRepository : IRegisterService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly EnotesDbContext enotesDbContext;

        public RegisterRepository(ApplicationDbContext applicationDbContext,EnotesDbContext enotesDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.enotesDbContext = enotesDbContext;
        }
        public async Task<int> check(string email)
        {
            
            Student? std = await enotesDbContext.Students.FirstOrDefaultAsync(e=>e.Email == email);
            Teacher? tcr = await enotesDbContext.Teachers.FirstOrDefaultAsync(e=>e.Email == email);
            
            if (std is not null)
            {
                return 1;
            }
            else if (tcr is not null)
            {
                return 2;
            }
            return 0;
        }

        public async Task post(string email,int test)
        {
            var data = await applicationDbContext.Users.FirstOrDefaultAsync(e=>e.Email == email);
            
            if (data is not null)
            {
                IdentityUserRole<String> identityUserRole = new IdentityUserRole<String>();
                identityUserRole.UserId = data.Id;
                identityUserRole.RoleId = Convert.ToString(test);
                applicationDbContext.UserRoles.Add(identityUserRole);
                applicationDbContext.SaveChanges();
            }
        }
    }
}
