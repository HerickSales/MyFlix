
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFlix.Data.Dtos;
using MyFlix.Services;

namespace MyFlix.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private UnitOfService _service;

        public UserController(UnitOfService service)
        {
            _service = service;
        }


        [HttpPost("Cadastro")]
        public async Task<IActionResult> CadastrarUser(CreateUserDto dto)
        {
            try
            {
                var result=await _service.UserService.CadastraUser(dto);
                if(result.IsSuccess)
                {
                    return Ok(result.Successes.FirstOrDefault());
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginDto)
        {
            var token = await _service.UserService.Logar(loginDto);
            if (token.IsSuccess)
            {
                return Ok(token.Successes.FirstOrDefault());
            }
            return BadRequest(token.Errors.FirstOrDefault());
        }
    }   
}
