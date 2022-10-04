using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IRepositries;

namespace UniqueTodoApplication.Implementation.Repositries
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
       public ItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsById(int id)
        {
           return await _context.Items.AnyAsync(d => d.Id == id && d.IsDeleted == false);
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Items.AnyAsync(b => b.Name.Equals(name) && b.IsDeleted == false);
        }

        public async Task<Item> GetByName(string name)
        {
           return await _context.Items.FirstOrDefaultAsync(t => t.Name.Equals(name) && t.IsDeleted == false);
        }

        public IList<Item> GetItemBySubcategory(int subcategoryId)
        {
            return _context.Items.Include(i => i.ItemSubcategories).ThenInclude(s => s.Subcategory).Where(t => t.ItemSubcategories.Any(t => t.SubcategoryId == subcategoryId)).ToList();
        }
    }
}