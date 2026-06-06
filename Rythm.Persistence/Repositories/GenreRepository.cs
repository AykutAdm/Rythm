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
    public class GenreRepository : IGenreRepository
    {
        private readonly RythmDbContext _context;

        public GenreRepository(RythmDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }

        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres.AsNoTracking().Include(x => x.Songs).ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(int id)
        {
            return await _context.Genres.AsNoTracking().Include(x => x.Songs).FirstOrDefaultAsync(x => x.GenreId == id);
        }

        public async Task UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
        }
    }
}
