using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<bool> ExistsByName(string name);

        Task<bool> ExistsById(int id);

        Task<Subcategory> GetByName(string name);

        IEnumerable<Subcategory> GetSelected(IList<int> ids);
    }
}