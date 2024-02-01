using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFlix.Models
{
    public class Videos
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Url { get; set; }


        [ForeignKey("CategoriaId")]
        public   int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }

  
    }
}
    