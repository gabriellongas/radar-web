
namespace Radar.Web.Api
{
    public interface IApiClient
    {
        bool DeletePessoa(int id, string token);
        bool DeletePost(int id, string token);
        LocalReadDto GetLocal(int id, string token);
        List<LocalReadDto> GetLocal(string token);
        PessoaReadDto GetPessoa(int id, string token);
        List<Pessoa> GetPessoa(string token);
        PostReadDto GetPost(int currentUserId, int id, string token);
        List<PostReadDto> GetPosts(int currentUserId, string token);
        List<PostReadDto> GetPostsFromLocal(int currentUserId, int localId, string token);
        List<PostReadDto> GetPostsFromPessoa(int currentUserId, int pessoaId, string token);
        void PostPessoa(PessoaCreateDto pessoa, string token);
        bool PostPost(PostCreateDto post, string token);
        bool PutPessoa(PessoaUpdateDto pessoa, string token);
        bool PutPost(Post post, string token);
        string SignIn(SignIn signIn);
        void UpdatePassword(UpdatePasswordDto updatePassword, string token);
        bool ValidatePassword(PessoaLoginDto login, string token);
    }
}