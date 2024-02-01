namespace MyFlix.Data.Dtos
{
    public class CreateVideoDto
    {
        public required string Titulo {get;set;}
        public required string Descricao { get; set; }
        public required  string Url  { get; set; }
        public int CategoriaId { get; set; }

    }
}
