using System.Threading.Tasks;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<bool> ExistsByName(string name);

        Task<bool> ExistsById(int id);

        Task<Role> GetByName(string name);
    }
}