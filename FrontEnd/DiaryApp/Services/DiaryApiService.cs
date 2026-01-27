using DiaryApp.Models.DTO;
using DiaryApp.Models.Errors;
using DiaryApp.Exceptions;
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
        public async Task<DiaryEntryDto?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<DiaryEntryDto>(
                $"api/DiaryEntries/{id}"
            );
        }

        public async Task CreateAsync(DiaryEntryDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/DiaryEntries", dto);

            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ApiValidationProblemDetailsDto>();

                if (problem != null && problem.Status == 400)
                    throw new ApiValidationException(problem);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task UpdateAsync(int id, DiaryEntryDto dto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/DiaryEntries/{id}", dto);

            if (!response.IsSuccessStatusCode)
            {
                var problem = await response.Content.ReadFromJsonAsync<ApiValidationProblemDetailsDto>();

                if (problem != null && problem.Status == 400)
                    throw new ApiValidationException(problem);

                response.EnsureSuccessStatusCode();
            }
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/DiaryEntries/{id}");
        }

    }
}
