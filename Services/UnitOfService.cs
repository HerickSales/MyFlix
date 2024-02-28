using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyFlix.Data.UnitOfWork;
using MyFlix.Models;

namespace MyFlix.Services
{
    public class UnitOfService
    {
        public VideoService VideoService { get; set; }
        public CategoriaService CategoriaService { get; set; }
        public UserService UserService { get; set; }


        public  IMapper mapper;
        public  UnitOfWork context;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IConfiguration _config;


        public UnitOfService(IMapper mapper, UnitOfWork context, UserManager<User> userManager,
            SignInManager<User> signInManager, IConfiguration config)
        {
            this.mapper = mapper;
            this.context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;

            this.VideoService = new VideoService(this.mapper, this.context);
            this.CategoriaService= new CategoriaService(this.mapper,this.context);
            this.UserService= new UserService(mapper, _userManager, _signInManager, _config);
        }



    }
}
