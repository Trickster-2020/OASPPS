using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class RoleServices : IRoleService
    {
        private readonly HttpClient httpClient;

        public RoleServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> Delete_Role(string id)
        {
            try
            {
                await httpClient.DeleteAsync("api/roles/"+id);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<RoleDto>> Get_Roles()
        {
            return await httpClient.GetFromJsonAsync<List<RoleDto>>("api/roles");
        }

        public async Task<bool> Post_Role(RoleDto role)
        {
           await httpClient.PostAsJsonAsync("api/roles",role);
            return true;
        }
    }
}
