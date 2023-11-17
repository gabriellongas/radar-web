namespace Radar.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
        public PessoaReadDto Pessoa { get; set; }
        public List<PostReadDto> Posts { get; set; }

        public ProfileViewModel(PessoaReadDto pessoa, List<PostReadDto> posts)
        {
            this.Pessoa = pessoa;
            this.Posts = posts;
        }
    }
}
