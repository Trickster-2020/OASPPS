
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;

using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class TeachServices : ITeachService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public TeachServices(HttpClient httpClient,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }
        public async Task<bool> Delete_Teach(string Id)
        {
            await httpClient.DeleteAsync("api/teaches/" + Id);
            navigationManager.NavigateTo("/teaches", true);
            return true;
        }

        public async Task<List<TeachDto>> Get_Teach()
        {
            return (await httpClient.GetFromJsonAsync<List<TeachDto>>("api/Teaches")).ToList();
        }

        public async Task<TeachDto> Get_Teach(string id)
        {
            return await httpClient.GetFromJsonAsync<TeachDto>("api/Teaches" + id);
            
        }

        public async Task<bool> Post_Teach(TeachDto teach)
        {
            if ((await httpClient.PostAsJsonAsync("api/Teaches", teach)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/teaches", true);
                return true;
            }
            return false;
        }

        public async Task<bool> Update_Teach(TeachDto teach)
        {
            if ((await httpClient.PutAsJsonAsync("api/Teaches", teach)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/teaches", true);
                return true;
            }
            return false;
        }
    }
}
