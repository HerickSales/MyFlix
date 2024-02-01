using MyFlix.Models;

namespace MyFlix.Data.Dtos
{
    public class ReadVideoDto
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Url { get; set; }
        public int CategoriaId { get; set; }

    }
}
