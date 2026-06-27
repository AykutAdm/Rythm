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
    public class UserLikedSongRepository : IUserLikedSongRepository
    {
        private readonly RythmDbContext _context;

        public UserLikedSongRepository(RythmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserLikedSong likedSong)
        {
            await _context.UserLikedSongs.AddAsync(likedSong);
        }

        public async Task<List<UserLikedSong>> GetByUserIdAsync(int userId)
        {
            return await _context.UserLikedSongs
            .AsNoTracking()
            .Include(x => x.Song)
            .ThenInclude(x => x.Artist)
            .Include(x => x.Song)
            .ThenInclude(x => x.Album)
            .Include(x => x.Song)
            .ThenInclude(x => x.Genre)
            .Where(x => x.AppUserId == userId)
            .ToListAsync();
        }

        public async Task<bool> IsLikedAsync(int userId, int songId)
        {
            return await _context.UserLikedSongs.AnyAsync(x => x.AppUserId == userId && x.SongId == songId);
        }

        public async Task RemoveAsync(int userId, int songId)
        {
            var likedSong = await _context.UserLikedSongs.FirstOrDefaultAsync(x => x.AppUserId == userId && x.SongId == songId);
            if (likedSong != null)
            {
                _context.UserLikedSongs.Remove(likedSong);
            }
        }
    }
}
