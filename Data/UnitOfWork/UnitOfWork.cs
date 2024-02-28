using MyFlix.Data.Context;
using MyFlix.Data.Repository;
using MyFlix.Models;

namespace MyFlix.Data.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly MyFlixContext _context;

        public Repository<Videos> VideoRepository { get; }
        public Repository<Categoria> CategoriaRepository { get; }

        public UnitOfWork(MyFlixContext context)
        {
            _context = context;
            VideoRepository = new Repository<Videos>(_context);
            CategoriaRepository = new Repository<Categoria>(_context);
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
