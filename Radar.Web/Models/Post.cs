namespace Radar.Web.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public Pessoa Pessoa { get; set; } = new();
        public Local Local { get; set; } = new();
        public string? Conteudo { get; set; }
        public int Avaliacao { get; set; }
        public DateTimeOffset DataPostagem { get; set; }
        public int Likes { get; set; }
    }
}
