namespace Radar.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Pessoa Pessoa { get; set; }
        public List<Post> Posts { get; set; }

        public ProfileViewModel(Pessoa pessoa, List<Post> posts)
        {
            this.Pessoa = pessoa;
            this.Posts = posts;
        }
    }
}
