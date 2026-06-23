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
    public class SongRepository : ISongRepository
    {
        private readonly RythmDbContext _context;

        public SongRepository(RythmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Song song)
        {
            await _context.Songs.AddAsync(song);
        }

        public async Task DeleteAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song != null)
            {
                _context.Songs.Remove(song);
            }
        }

        public async Task<List<Song>> GetAllAsync()
        {
            return await _context.Songs.AsNoTracking().Include(x => x.Artist).Include(x => x.Album).Include(x => x.Genre).ToListAsync();
        }

        public async Task<Song> GetByIdAsync(int id)
        {
            return await _context.Songs.Include(x => x.Artist).Include(x => x.Album).Include(x => x.Genre).FirstOrDefaultAsync(x => x.SongId == id);
        }

        public async Task UpdateAsync(Song song)
        {
            _context.Songs.Update(song);
        }
    }
}
