using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class StudentServices : IStudentService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public StudentServices(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }
        public async Task<bool> Delete_Student(string Id)
        {
            await httpClient.DeleteAsync("api/students/" + Id);
            navigationManager.NavigateTo("/students", true);
            return true;
        }

        public async Task<StudentDto> Get_Student(string id)
        {
            return await httpClient.GetFromJsonAsync<StudentDto>("api/Students/" + id);
        }

        public async Task<List<StudentDto>> Get_Students()
        {
            return (await httpClient.GetFromJsonAsync<List<StudentDto>>("api/Students")).ToList();
        }

        public async Task<bool> Post_Student(StudentDto student)
        {
            if ((await httpClient.PostAsJsonAsync("api/Students", student)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/students", true);
                return true;
            }
            return false;
        }

        public async Task<bool> Update_Student(StudentDto student)
        {
            if ((await httpClient.PutAsJsonAsync("api/Students", student)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/students", true);
                return true;
            }
            return false;
        }
    }
}
