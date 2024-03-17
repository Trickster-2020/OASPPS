using ENotes.Client.Pages;
using ENotes.shared.Contracts;
using ENotes.shared.DTOs;
using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Json;

namespace ENotes.Client.Services
{
    public class NoteServices : INoteService
    {
        private readonly HttpClient httpClient;
        private readonly NavigationManager navigationManager;

        public NoteServices(HttpClient httpClient,NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.navigationManager = navigationManager;
        }
        public async Task<bool> Delete_Note(string Id)
        {
            await httpClient.DeleteAsync("api/notes/" + Id);
            navigationManager.NavigateTo("/notes", true);
            return true;
        }

        public async Task<List<NoteDto>> Get_Note()
        {
            return (await httpClient.GetFromJsonAsync<List<NoteDto>>("api/Notes")).ToList();
        }

        public async Task<List<NoteDto>> Get_Note(string id)
        {
            
            return (await httpClient.GetFromJsonAsync<List<NoteDto>>("api/Notes/" + id)).ToList();
        }

        public async Task<List<NoteDto>> Get_Notes_By_Email(string Email)
        {
            return (await httpClient.GetFromJsonAsync<List<NoteDto>>("api/Notes/email" + Email)).ToList();
        }

        public async Task<bool> Post_Note(NoteDto note)
        {
            if ((await httpClient.PostAsJsonAsync("api/Notes", note)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/notes", true);
                return true;
            }
            return false;
        }

        public async Task<bool> Update_Note(NoteDto note)
        {
            if ((await httpClient.PutAsJsonAsync("api/Notes", note)).IsSuccessStatusCode)
            {
                navigationManager.NavigateTo("/notes", true);
                return true;
            }
            return false;
        }
    }
}
