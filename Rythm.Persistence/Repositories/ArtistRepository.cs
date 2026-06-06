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
    public class ArtistRepository : IArtistRepository
    {
        private readonly RythmDbContext _context;

        public ArtistRepository(RythmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
        }

        public async Task DeleteAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
            }

        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _context.Artists.AsNoTracking().Include(x => x.Albums).Include(x => x.Songs).ToListAsync();
        }

        public async Task<Artist> GetByIdAsync(int id)
        {
            return await _context.Artists.AsNoTracking().Include(x => x.Albums).Include(x => x.Songs).FirstOrDefaultAsync(x => x.ArtistId == id);
        }

        public async Task UpdateAsync(Artist artist)
        {
            _context.Artists.Update(artist);
        }
    }
}
