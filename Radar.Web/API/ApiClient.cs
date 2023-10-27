using Radar.Web.Models;
using System.Text;
using System.Text.Json;

namespace Radar.Web.Api
{
    public class ApiClient
    {
        private static readonly HttpClient _client = new() { BaseAddress = new Uri("https://localhost:7118") };        
        private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

        #region Pessoa
        public List<Pessoa> GetPessoa()
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync("/api/Pessoas").Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<Pessoa>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return new List<Pessoa>();
            }
        }

        public Pessoa GetPessoa(int id)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync($"/api/Pessoas/{id}").Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<Pessoa>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return new Pessoa();
            }
        }

        public bool PostPessoa(Pessoa pessoa)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(pessoa);

                HttpResponseMessage response = _client.PostAsync("/api/Pessoas", new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return false;
            }
        }

        public bool PutPessoa(Pessoa pessoa)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(pessoa);

                HttpResponseMessage response = _client.PutAsync("/api/Pessoas", new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return false;
            }
        }

        public bool DeletePessoa(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync($"/api/Pessoas/{id}").Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return false;
            }
        }
        #endregion

        #region Seguidores
        #endregion

        #region Local
        #endregion

        #region Post
        public List<Post> GetPosts()
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync("/api/Posts").Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<Post>>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return new List<Post>();
            }
        }

        public Post GetPost(int id)
        {
            try
            {
                HttpResponseMessage response = _client.GetAsync($"/api/Posts/{id}").Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<Post>(jsonContent, _options) ?? new();
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return new Post();
            }
        }

        public bool PostPost(Post post)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(post);

                HttpResponseMessage response = _client.PostAsync("/api/Posts", new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return false;
            }
        }

        public bool PutPost(Post post)
        {
            try
            {
                string jsonContent = JsonSerializer.Serialize(post);

                HttpResponseMessage response = _client.PutAsync("/api/Posts", new StringContent(jsonContent, Encoding.UTF8, "application/json")).Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return false;
            }
        }

        public bool DeletePost(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync($"/api/Posts/{id}").Result;
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                File.AppendAllLines("log.txt", new List<string> { ex.Message });
                return false;
            }
        }
        #endregion
    }
}
