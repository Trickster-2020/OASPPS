using ENotes.Client.Pages;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class ScoreServices : IScoreServices
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public ScoreServices(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }

        public async Task<bool> Delete_Score(string Id)
        {
            await httpClient.DeleteAsync("api/score/" + Id);
            navigationManager.NavigateTo("/scores", true);
            return true;
        }

        public async  Task<ScoreDto> Get_Score(string id)
        {
            return await httpClient.GetFromJsonAsync<ScoreDto>("api/score" + id);
        }

        public async Task<List<ScoreDto>> Get_Scores()
        {
            return (await httpClient.GetFromJsonAsync<List<ScoreDto>>("api/score")).ToList();
        }

        public async Task<bool> Post_Score(ScoreDto score)
        {
            if ((await httpClient.PostAsJsonAsync("api/score", score)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/scores", true);
                return true;
            }
            return false;
        }

        public async Task<bool> Update_Score(ScoreDto score)
        {
            if ((await httpClient.PutAsJsonAsync("api/score", score)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/scores", true);
                return true;
            }
            return false;
        }
    }
}
