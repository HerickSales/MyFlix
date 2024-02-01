using System.ComponentModel.DataAnnotations;

namespace MyFlix.Data.Dtos
{
    public class CreateUserDto
    {
        [Required]
            
        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }
        [Compare("Password")]
        public string RePassword { get; set; }

    }
}
