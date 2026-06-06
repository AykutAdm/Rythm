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
    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly RythmDbContext _context;

        public PlaylistRepository(RythmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
        }

        public async Task DeleteAsync(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
            }

        }

        public async Task<List<Playlist>> GetAllAsync()
        {
            return await _context.Playlists.AsNoTracking().ToListAsync();
        }

        public async Task<Playlist> GetByIdAsync(int id)
        {
            return await _context.Playlists.AsNoTracking().Include(x => x.PlaylistSongs).ThenInclude(x => x.Song).FirstOrDefaultAsync(x => x.PlaylistId == id);
        }

        public async Task UpdateAsync(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
        }
    }
}
