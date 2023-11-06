using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Radar.Web.Api
{
    public class ApiClient
    {
        private static readonly HttpClient _client = new() { BaseAddress = new Uri("apiradar-hml.azurewebsites.net") };
        private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
        private const string TOKEN = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW5pc3RyYWRvciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQGdtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMTEiLCJleHAiOjE2OTkzNjUwNDd9.NWPi8PivtGIrFDfSYzfNyI80uDbO-d_UKr6LuWWlZ3TBYz4MV6taE0hnYGgUXGRHiAh0OdZ_bvGBVZaX952LtA";

        #region Pessoa
        public string SignIn(SignIn signIn)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(signIn.ToSignInDto());

                HttpResponseMessage response = _client.PostAsync("/api/Pessoa/SignIn", new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();

                response.EnsureSuccessStatusCode();

                string token = response.Content.ReadAsStringAsync().Result;

                return token;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public List<Pessoa> GetPessoa()
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, "/api/Pessoa");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<Pessoa>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public Pessoa GetPessoa(int id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"/api/Pessoa/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<Pessoa>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool PostPessoa(Pessoa pessoa)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Post, "/api/Pessoa");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                request.Content = new StringContent(JsonSerializer.Serialize(pessoa), Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool PutPessoa(Pessoa pessoa)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Put, "/api/Pessoa");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                request.Content = new StringContent(JsonSerializer.Serialize(pessoa), Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool DeletePessoa(int id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Delete, $"/api/Pessoa/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }
        #endregion

        #region Seguidores
        #endregion

        #region Local
        public List<Local> GetLocal()
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, "/api/Local");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<Local>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }
        #endregion

        #region Post
        public List<Post> GetPosts()
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, "/api/Post");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<Post>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }
        public List<Post> GetPostsFromPessoa(int pessoaId)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"/api/Post/FromPessoa/{pessoaId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<Post>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public Post GetPost(int id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"/api/Post/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<Post>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool PostPost(PostCreateDto post)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Post, "/api/Post");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                request.Content = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool PutPost(Post post)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Put, "/api/Post");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);
                request.Content = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool DeletePost(int id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Delete, $"/api/Post/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }
        #endregion
    }
}
