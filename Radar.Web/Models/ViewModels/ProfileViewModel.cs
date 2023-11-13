namespace Radar.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Pessoa Pessoa { get; set; }
        public List<PostReadDto> Posts { get; set; }

        public ProfileViewModel(Pessoa pessoa, List<PostReadDto> posts)
        {
            this.Pessoa = pessoa;
            this.Posts = posts;
        }
    }
}
