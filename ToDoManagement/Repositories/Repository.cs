using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoManagement.Enums;
using ToDoManagement.Models;

namespace ToDoManagement.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly Context context;
        internal DbSet<T> dbSet;
        public Repository(Context _context)
        {
            context = _context;
            this.dbSet = context.Set<T>();
        }
        public void Delete(T entity)
        {

            dbSet.Remove(entity);
        }
        public List<T> GetAll()
        {
            IQueryable<T> query = dbSet;

            return query.ToList();
        }
        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
        public void Insert(T obj)
        {
            dbSet.Add(obj);
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public void Update(T obj)
        {
            dbSet.Update(obj);
        }

        public IQueryable<T> FilterTodos(IQueryable<T> query, string status, string priority, DateTime? startDate, DateTime? endDate)
        {
            if (!string.IsNullOrEmpty(status) && Enum.TryParse(status, out TodoStatus todoStatus))
            {
                query = query.Where(t => ((Todo)(object)t).Status == todoStatus);
            }

            if (!string.IsNullOrEmpty(priority) && Enum.TryParse(priority, out TodoPriority todoPriority))
            {
                query = query.Where(t => ((Todo)(object)t).Priority == todoPriority);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(t => ((Todo)(object)t).CreatedDate >= startDate.Value && ((Todo)(object)t).CreatedDate <= endDate.Value);
            }

            return query;
        }
    }
}
