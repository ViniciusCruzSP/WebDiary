using DiaryApp.Models.DTO;
using DiaryApp.Models.Errors;
using DiaryApp.Exceptions;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using DiaryApp.Models.Auth;

namespace DiaryApp.Services
{
    public class DiaryApiService
    {
        private readonly HttpClient _httpClient;

        public DiaryApiService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("DiaryApi");
        }

        public async Task<string?> LoginAsync(string email, string password)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login",new {Email = email, Password = password });

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
            return result?.Token;
        }

        public async Task<List<DiaryEntryDto>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<DiaryEntryDto>>(
                "api/DiaryEntries"
            );
        }
        public async Task<DiaryEntryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/DiaryEntries/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new ApiNotFoundException("Diary Entry not found.");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<DiaryEntryDto>();
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
