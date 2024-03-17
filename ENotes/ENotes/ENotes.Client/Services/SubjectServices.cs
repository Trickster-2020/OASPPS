using ENotes.Client.Pages;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class SubjectServices : ISubjectService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public SubjectServices(HttpClient httpClient,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task<bool> Delete_Subject(string Id)
        {
            await httpClient.DeleteAsync("api/Subjects/"+ Id);
            navigationManager.NavigateTo("/subjects", true);
            return true;
        }

        public async Task<List<SubjectDto>> GetSubjectBySemester(string SemesterId)
        {
            return (await httpClient.GetFromJsonAsync<List<SubjectDto>>("api/Subjects/"+SemesterId)).ToList();
        }

        public async Task<List<SubjectDto>> Get_Subject()
        {
            return (await httpClient.GetFromJsonAsync<List<SubjectDto>>("api/Subjects")).ToList();
        }



        public async Task<bool> Post_Subject(SubjectDto subject)
        {
            if ((await httpClient.PostAsJsonAsync("api/Subjects", subject)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/subjects",true);
                return true;
            }
            return false;
  
        }

        public async Task<bool> Update_Subject(SubjectDto subject)
        {
            if ((await httpClient.PutAsJsonAsync("api/Subjects", subject)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/subjects", true);
                return true;
            }
            return false;
        }
    }
}
