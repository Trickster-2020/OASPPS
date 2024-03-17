using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class QuizServices : IQuizService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public QuizServices(HttpClient httpClient,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }
        public async Task<bool> Delete_Quiz(string Id)
        {
            await httpClient.DeleteAsync("api/Quizes/" + Id);
            navigationManager.NavigateTo("/quizes", true);
            return true;
        }

        public async Task<List<QuizDto>> Get_Quiz()
        {
            return (await httpClient.GetFromJsonAsync<List<QuizDto>>("api/Quizes")).ToList();
        }

        public async Task<List<QuizDto>> Get_Quiz(string id)
        {
            return (await httpClient.GetFromJsonAsync<List<QuizDto>>("api/Quizes"+id)).ToList();
        }

        public async Task<bool> Post_Quiz(QuizDto quiz)
        {
            if ((await httpClient.PostAsJsonAsync("api/Quizes", quiz)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/quizes", true);
                return true;
            }
            return false;
        }

        public async Task<bool> Update_Quiz(QuizDto quiz)
        {
            if ((await httpClient.PutAsJsonAsync("api/Quizes", quiz)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/quizes", true);
                return true;
            }
            return false;
        }
    }
}
