using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Interfaces
{
    public interface IListeningHistoryRepository
    {
        Task<List<ListeningHistory>> GetAllAsync();
        Task AddAsync(ListeningHistory history);
        Task<List<ListeningHistory>> GetByUserIdAsync(int userId);
    }
}
