using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;

namespace UniqueTodoApplication.Implementation.Repositries
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _context.Admins.AnyAsync(e => e.Email.Equals(email) && e.IsDeleted == false);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Admins.AnyAsync(d => d.Id == id && d.IsDeleted == false);
        }

        public async Task<Admin> GetByEmail(string email)
        {
            return await _context.Admins.SingleOrDefaultAsync(d => d.Email.Equals(email) && d.IsDeleted == false);
        }
    }
}