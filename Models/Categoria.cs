using System.ComponentModel.DataAnnotations;

namespace MyFlix.Models
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id {  get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Cor {  get; set; }

        public virtual  ICollection<Videos>? Videos { get; set; } 

    }
}
