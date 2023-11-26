namespace Radar.Web.Models.ViewModels
{
    public class ProfileViewModel
    {
        public PessoaReadDto Pessoa { get; set; } = null!;
        public IEnumerable<PostReadDto> Posts { get; set; } = null!;
        public bool IsMe { get; set; }
    }
}
