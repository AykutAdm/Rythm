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
    public class AlbumRepository : IAlbumRepository
    {
        private readonly RythmDbContext _context;

        public AlbumRepository(RythmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Album album)
        {
            await _context.Albums.AddAsync(album);
        }

        public async Task DeleteAsync(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            if (album != null)
            {
                _context.Albums.Remove(album);
            }

        }

        public async Task<List<Album>> GetAllAsync()
        {
            return await _context.Albums.AsNoTracking().Include(x => x.Artist).ToListAsync();
        }

        public async Task<Album> GetByIdAsync(int id)
        {
            return await _context.Albums.AsNoTracking().Include(x => x.Artist).Include(x => x.Songs).FirstOrDefaultAsync(x => x.AlbumId == id);
        }

        public async Task UpdateAsync(Album album)
        {
            _context.Albums.Update(album);
        }
    }
}
