using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using TestA.Models;

namespace TestA.Services
{
    class HackerEarthService
    {
        private const string ApiUrl =
            "https://api.hackerearth.com/v4/partner/code-evaluation/submissions/";

        private const string ApiKey = ""; // API key should be provided locally

        private readonly HttpClient _httpClient = new HttpClient();

        public async Task<string> ExecuteCodeAsync(
            string code,
            string language,
            int timeLimit,
            int memoryLimit)
        {
            try
            {
                var request = new CodeExecutionRequest
                {
                    source = code,
                    lang = MapLanguage(language),
                    time_limit = timeLimit,
                    memory_limit = memoryLimit
                };


                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                _httpClient.DefaultRequestHeaders.Clear();

                if (!string.IsNullOrWhiteSpace(ApiKey))
                {
                    _httpClient.DefaultRequestHeaders.Add(
                        "Authorization",
                        $"Bearer {ApiKey}");
                }

                var response = await _httpClient.PostAsync(ApiUrl, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return $"HTTP Error: {response.StatusCode}\n{responseJson}";
                }

                var result = JsonSerializer.Deserialize<CodeExecutionResult>(responseJson);

                if (result?.run_status?.stderr != null)
                    return "Error:\n" + result.run_status.stderr;

                return result?.run_status?.output ?? "No output";
            }
            catch (Exception ex)
            {
                return $"Exception: {ex.Message}";
            }
         }

        private string MapLanguage(string language)
        {
            return language switch
            {
                "C#" => "csharp",
                "Python" => "python3",
                "JavaScript" => "javascript",
                _ => "csharp"
            };
        }
    }
}