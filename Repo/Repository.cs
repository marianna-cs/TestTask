using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Repo
{
    public abstract class Repository<T> where T : class
    {
        protected BaseContext _context;
        public Repository(BaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllList()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public void Add(T entity)
        {
            _context.Add(entity);

        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Remove<T>(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

    }
}
