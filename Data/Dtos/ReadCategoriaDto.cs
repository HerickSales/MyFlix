namespace MyFlix.Data.Dtos
{
    public class ReadCategoriaDto
    {
        public string Titulo {  get; set; }
        public string Cor { get; set;}

        public ICollection<ReadVideoDto> Videos { get; set;}
    }
}
