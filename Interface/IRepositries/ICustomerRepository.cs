using System.Threading.Tasks;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<bool> ExistsByEmail(string email);

        Task<bool> ExistsById(int id);

        Task<Customer> GetByEmail(string email);
    }
}