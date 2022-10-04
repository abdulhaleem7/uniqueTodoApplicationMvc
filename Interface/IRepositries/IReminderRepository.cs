using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.Entities;

namespace UniqueTodoApplication.Interface.IRepositries
{
    public interface IReminderRepository : IRepository<Reminder>
    {
      public Task<IEnumerable<Reminder>> GetPendingReminder();
        
    }
}