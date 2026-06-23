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

        public async Task AddSongAsync(PlaylistSong playlistSong)
        {
            await _context.PlaylistSongs.AddAsync(playlistSong);
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
            return await _context.Playlists.AsNoTracking()
       .Include(x => x.PlaylistSongs)
           .ThenInclude(x => x.Song)
               .ThenInclude(x => x.Artist)
       .Include(x => x.PlaylistSongs)
           .ThenInclude(x => x.Song)
               .ThenInclude(x => x.Album)
       .Include(x => x.PlaylistSongs)
           .ThenInclude(x => x.Song)
               .ThenInclude(x => x.Genre)
       .FirstOrDefaultAsync(x => x.PlaylistId == id);
        }

        public async Task<List<Playlist>> GetByUserIdAsync(int userId)
        {
            return await _context.Playlists.AsNoTracking().Include(x => x.PlaylistSongs).ThenInclude(x => x.Song).Where(x => x.AppUserId == userId).ToListAsync();
        }

        public async Task RemoveSongAsync(int playlistId, int songId)
        {
            var playlistSong = await _context.PlaylistSongs.FirstOrDefaultAsync(x => x.PlaylistId == playlistId && x.SongId == songId);
            if (playlistSong != null)
            {
                _context.PlaylistSongs.Remove(playlistSong);
            }
        }

        public async Task UpdateAsync(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
        }
    }
}
