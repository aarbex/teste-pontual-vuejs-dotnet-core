using System.Threading.Tasks;
using Api.Models;

namespace Api.Data
{
    public interface IRepository
    {
        //Geral
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;

         Task<bool> SaveChangesAsync();

         //Clientes
         Task<Cliente[]> GetClientes();

         Task<Cliente> GetClienteById(long ClienteId);
    }
}