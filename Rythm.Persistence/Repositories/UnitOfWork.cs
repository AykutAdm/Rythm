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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RythmDbContext _context;

        public ISongRepository Songs { get; }
        public IArtistRepository Artists { get; }
        public IAlbumRepository Albums { get; }
        public IGenreRepository Genres { get; }
        public IPlaylistRepository Playlists { get; }

        public UnitOfWork(RythmDbContext context)
        {
            _context = context;
            Songs = new SongRepository(context);
            Artists = new ArtistRepository(context);
            Albums = new AlbumRepository(context);
            Genres = new GenreRepository(context);
            Playlists = new PlaylistRepository(context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
