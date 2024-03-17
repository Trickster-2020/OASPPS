using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ENotes.Controlers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService roleService)
        {
            this.roleService = roleService;
        }
        [HttpPost]
        public async Task<ActionResult> AddRole(RoleDto roleDto)
        {
            bool check =  await roleService.Post_Role(roleDto);
            if (!check)
            {
                return Content("Error");
            }
            else
            {
                return Content("Success");
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<RoleDto>>> GetRoles()
        {
            List<RoleDto> roles = new List<RoleDto>();
            roles = await roleService.Get_Roles();
            return roles;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSemester(string id)
        {
             
            if (await roleService.Delete_Role(id))
            {
                return Content($"{id} has been deleted.");
            }

            return Content($"{id} not found in database.");
        }

    }
}
