using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public TeacherService(HttpClient httpClient,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        

        public async Task<bool> Delete_Teacher(string Id)
        {
            await httpClient.DeleteAsync("api/teachers/" + Id);
            navigationManager.NavigateTo("/teachers", true);
            return true;
        }

        public async Task<TeacherDto> Get_Teacher(string id)
        {
            return await httpClient.GetFromJsonAsync<TeacherDto>("api/Teachers/"+ id);
        }

        public async Task<List<TeacherDto>> Get_Teachers()
        {
            return (await httpClient.GetFromJsonAsync<List<TeacherDto>>("api/Teachers")).ToList();
        }

        public async Task<bool> Post_Teacher(TeacherDto teacher)
        {
            if ((await httpClient.PostAsJsonAsync("api/Teachers", teacher)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/teachers", true);
                return true;
            }
            return false;
        }

        public async Task<bool> Update_Teacher(TeacherDto teacher)
        {
            if ((await httpClient.PutAsJsonAsync("api/Teachers", teacher)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/teachers",true);
                return true;
            }
            return false;
        }
    }
}
