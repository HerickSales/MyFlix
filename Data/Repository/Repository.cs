using FluentResults;
using MyFlix.Data.Context;

namespace MyFlix.Data.Repository
{
    public class Repository<T> where T: class
    {

        private readonly MyFlixContext context;

        public Repository(MyFlixContext context)
        {
            this.context = context;
        }

        public List<T> Get(int pageNumber, int pageSize)
        {
            
            return context.Set<T>().Skip((pageNumber-1) * pageSize).Take(pageSize).ToList();
        }

        public List<T> GetAll() { 
            return context.Set<T>().ToList();
        }
        public void Add(T item)
        {
            context.Set<T>().Add(item);
        }

        public void Delete(T item)
        {
            context.Remove(item);
        }

    }
}
