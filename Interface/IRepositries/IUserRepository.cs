using System.Threading.Tasks;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> ExistsByEmail(string email);

        Task<bool> ExistsById(int id);

        Task<User> GetByEmail(string email);
    }
}