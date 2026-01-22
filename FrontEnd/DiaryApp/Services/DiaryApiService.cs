using DiaryApp.Models.DTO;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace DiaryApp.Services
{
    public class DiaryApiService
    {
        private readonly HttpClient _httpClient;

        public DiaryApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("DiaryApi");
        }

        public async Task<List<DiaryEntryDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DiaryEntryDto>>(
                "api/DiaryEntries"
            );
        }

        public async Task CreateAsync(DiaryEntryDto dto)
        {
            await _httpClient.PostAsJsonAsync("api/DiaryEntries", dto);
        }

        public async Task UpdateAsync(int id, DiaryEntryDto dto)
        {
            await _httpClient.PutAsJsonAsync($"api/DiaryEntries/{id}", dto);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/DiaryEntries/{id}");
        }

    }
}
