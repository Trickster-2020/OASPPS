using ENotes.Client.Pages;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class SemesterService : ISemesterService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public SemesterService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }
        public async Task<bool> Delete_Semester(string id)
        {
            await httpClient.DeleteAsync("api/semesters/" + id);
            navigationManager.NavigateTo("/semesters", true);
            return true;
        }

        public async Task<List<SemesterDto>> Get_Semester()
        {
            List<SemesterDto>? semesterDto = new List<SemesterDto>();
            semesterDto = await httpClient.GetFromJsonAsync<List<SemesterDto>>("api/semesters");
            return semesterDto.ToList();

        }

        public async Task<bool> Post_Semester(SemesterDto semester)
        {
            if ((await httpClient.PostAsJsonAsync("api/semesters", semester)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/semesters", true);
                return true;
            }
            return false;

        }

        public async Task<bool> Update_Semester(SemesterDto semester)
        {
            if ((await httpClient.PutAsJsonAsync("api/semesters", semester)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/semesters", true);
                return true;
            }
            return false;
        }
    }
}
