using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Radar.Web.Api
{
    public class ApiClient : IApiClient
    {
        private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
        private IConfiguration _configuration;
        public ApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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

                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                string jsonContent = JsonSerializer.Serialize(signIn.ToSignInDto());

                HttpResponseMessage response = client.PostAsync(SignInPath, new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;

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

        public bool ValidatePassword(PessoaLoginDto login, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Post, $"{PessoaPath}/ValidatePassword");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(JsonSerializer.Serialize(login), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;

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

        public void UpdatePassword(UpdatePasswordDto updatePassword, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Put, $"{PessoaPath}/UpdatePassword");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(JsonSerializer.Serialize(updatePassword), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;

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

        public List<Pessoa> GetPessoa(string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, PessoaPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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

        public PessoaReadDto GetPessoa(int id, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, $"{PessoaPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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

        public void PostPessoa(PessoaCreateDto pessoa, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Post, PessoaPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(JsonSerializer.Serialize(pessoa), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;

                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool PutPessoa(PessoaUpdateDto pessoa, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Put, $"{PessoaPath}/{pessoa.PessoaId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(JsonSerializer.Serialize(pessoa), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool DeletePessoa(int id, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Delete, $"{PessoaPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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
        public List<LocalReadDto> GetLocal(string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, LocalPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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
        public LocalReadDto GetLocal(int id, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, $"{LocalPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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
        public List<PostReadDto> GetPosts(int currentUserId, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/{currentUserId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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

        public List<PostReadDto> GetPostsFromPessoa(int currentUserId, int pessoaId, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/FromPessoa/{currentUserId}/{pessoaId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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

        public List<PostReadDto> GetPostsFromLocal(int currentUserId, int localId, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/FromLocal/{currentUserId}/{localId}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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

        public PostReadDto GetPost(int currentUserId, int id, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Get, $"{PostPath}/{currentUserId}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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

        public bool PostPost(PostCreateDto post, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Post, PostPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool PutPost(Post post, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Put, PostPath);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Content = new StringContent(JsonSerializer.Serialize(post), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.SendAsync(request).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                throw;
            }
        }

        public bool DeletePost(int id, string token)
        {
            try
            {
                HttpClient client = new HttpClient() { BaseAddress = new Uri(_configuration["ApiSettings:ApiURL"]) };
                HttpRequestMessage request = new(HttpMethod.Delete, $"{PostPath}/{id}");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = client.SendAsync(request).Result;
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
