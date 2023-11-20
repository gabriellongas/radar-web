using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Radar.Web.Api
{
    public class ApiClient
    {
        internal static readonly string Token = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW5pc3RyYWRvciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImFkbWluQHJhZGFyLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiMSIsImV4cCI6MTcwMDU3NTQ4Nn0.zviEao73DeUKb75WK8ZlemgbwUfo-4Fpa-tpPRA3X5_UF0vfWzGXsO8e3rGw-u2vdd2WGnfImZv31kmEIUR0uw";
        internal static readonly string Origin = "https://localhost:7118";
        private static readonly HttpClient _client = new() { BaseAddress = new Uri(Origin) };
        private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

        #region Path
        internal static readonly string CurtidaPath = "/api/Curtida";
        internal static readonly string LocalPath = "/api/Local";
        internal static readonly string PessoaPath = "/api/Pessoa";
        internal static readonly string PostPath = "/api/Post";
        internal static readonly string SeguidoresPath = "/api/Seguidores";
        internal static readonly string SignInPath = "/api/Pessoa/SignIn";
        #endregion Path

        #region Pessoa
        public string SignIn(SignIn signIn)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(signIn.ToSignInDto());

                HttpResponseMessage response = _client.PostAsync(SignInPath, new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;

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

        public bool ValidatePassword(PessoaLoginDto login)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Post, $"{PessoaPath}/ValidatePassword");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                request.Content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.SendAsync(request).Result;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();

                response.EnsureSuccessStatusCode();

                bool isValid = JsonSerializer.Deserialize<bool>(response.Content.ReadAsStringAsync().Result, _options);

                return isValid;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public void UpdatePassword(UpdatePasswordDto updatePassword)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Put, $"{PessoaPath}/UpdatePassword");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                request.Content = new StringContent(JsonSerializer.Serialize(updatePassword), Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.SendAsync(request).Result;

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException();

                response.EnsureSuccessStatusCode();
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
                HttpRequestMessage request = new(HttpMethod.Get, PessoaPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

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

        public PessoaReadDto GetPessoa(int id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"{PessoaPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<PessoaReadDto>(jsonContent, _options) ?? new();
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
                HttpRequestMessage request = new(HttpMethod.Post, PessoaPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
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

        public bool PutPessoa(PessoaUpdateDto pessoa)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Put, $"{PessoaPath}/{pessoa.PessoaId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
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
                HttpRequestMessage request = new(HttpMethod.Delete, $"{PessoaPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

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
        public List<LocalReadDto> GetLocal()
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, LocalPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<LocalReadDto>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }
        public LocalReadDto GetLocal(int id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"{LocalPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<LocalReadDto>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }
        #endregion

        #region Post
        public List<PostReadDto> GetPosts(int currentUserId)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/{currentUserId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<PostReadDto>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public List<PostReadDto> GetPostsFromPessoa(int currentUserId, int pessoaId)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/FromPessoa/{currentUserId}/{pessoaId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<PostReadDto>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public List<PostReadDto> GetPostsFromLocal(int currentUserId, int localId)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/FromLocal/{currentUserId}/{localId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<PostReadDto>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public PostReadDto GetPost(int currentUserId, int id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/{currentUserId}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

                HttpResponseMessage response = _client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<PostReadDto>(jsonContent, _options) ?? new();
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
                HttpRequestMessage request = new(HttpMethod.Post, PostPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
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
                HttpRequestMessage request = new(HttpMethod.Put, PostPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);
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
                HttpRequestMessage request = new(HttpMethod.Delete, $"{PostPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

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
