using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class Repository : IRepository
    {
        public DataContext _context { get; }
        public Repository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Cliente> GetClienteById(long ClienteId)
        {
            IQueryable<Cliente> query = _context.Clientes;

            query = query.AsNoTracking()
                        .OrderBy(c => c.Id)
                        .Where(cliente => cliente.Id == ClienteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Cliente[]> GetClientes()
        {
            IQueryable<Cliente> query = _context.Clientes;

            query = query.AsNoTracking().OrderBy(c => c.Id);

            return await query.ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
        
    }
}