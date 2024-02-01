namespace MyFlix.Data.Dtos
{
    public class UpdateVideoDto
    {
        public required string Titulo {get;set;}
        public required string Descricao { get; set; }
        public required  string Url  { get; set; }

        public required int CategoriaId { get; set; }


    }
}
