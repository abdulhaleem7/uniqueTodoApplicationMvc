using System.Threading.Tasks;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<bool> ExistsByEmail(string email);

        Task<bool> ExistsById(int id);

        Task<Admin> GetByEmail(string email);
    }
}