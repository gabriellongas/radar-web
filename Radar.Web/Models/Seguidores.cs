namespace Radar.Web.Models
{
    public class Seguidores
    {
        public int SeguidorID { get; set; }
        public Pessoa Pessoa { get; set; } = new();
        public Pessoa Seguidor { get; set; } = new();
    }
}
