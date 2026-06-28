using Microsoft.EntityFrameworkCore;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using Rythm.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Persistence.Repositories
{
    public class ListeningHistoryRepository : IListeningHistoryRepository
    {
        private readonly RythmDbContext _context;

        public ListeningHistoryRepository(RythmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ListeningHistory history)
        {
            await _context.ListeningHistories.AddAsync(history);
        }

        public async Task<List<ListeningHistory>> GetAllAsync()
        {
            return await _context.ListeningHistories.AsNoTracking().ToListAsync();
        }

        public async Task<List<ListeningHistory>> GetByUserIdAsync(int userId)
        {
            return await _context.ListeningHistories.Include(x => x.Song).ThenInclude(x => x.Artist).Where(x => x.AppUserId == userId).OrderByDescending(x => x.ListenedAt).ToListAsync();
        }
    }
}
