using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFlix.Models;

namespace MyFlix.Data.Context
{
    public class MyFlixContext : IdentityDbContext<User>
    {
        public MyFlixContext(DbContextOptions<MyFlixContext> opts) : base(opts)
        {

        }
        public DbSet<Videos> Videos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
