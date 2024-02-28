
using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyFlix.Data.Dtos;
using MyFlix.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MyFlix.Services
{
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IConfiguration _config;

        public UserService(IMapper map, UserManager<User> user, SignInManager<User> signInManager, IConfiguration config)
        {
            _mapper = map;
            _userManager = user;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<Result> CadastraUser(CreateUserDto userDto)
        {
            Result result;
            try
            {
                var user = _mapper.Map<User>(userDto);
                var register = await _userManager.CreateAsync(user, userDto.Password);
                if (!register.Succeeded)
                {
                    result = new Result().WithError(new Error(register.Errors.FirstOrDefault().Description));
                    return result;
                }
                result = new Result().WithSuccess(new Success("Usuario Criado"));
                return result;

            }catch (Exception ex)
            {
                result= new Result().WithError(ex.Message);
                return result;
            }
        }

        public async Task<Result> Logar(LoginUserDto loginDto)
        {
            Result result;
            try
            {
                
                var resultSign = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);

                if (!resultSign.Succeeded)
                {
                    throw new ApplicationException("Login ou Senha incorreto");
                }
                var token = GerarToken(loginDto);
                result = new Result().WithSuccess(new Success("Sucesso").WithMetadata("token", token));

                return result;
            }
            catch (Exception ex)
            {   
                result= new Result().WithError(new Error(ex.Message));
                return result;

            }

        }
        
        public string GerarToken(LoginUserDto dto)
        { 
           
                Claim[] claims = new Claim[]
                {
                new Claim("UserName", dto.Username)

                };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config ["Jwt:Key"]));


            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(

                    expires: DateTime.Now.AddMinutes(360),
                    claims: claims,
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    signingCredentials: credenciais

                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
