using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ISongRepository Songs { get; }
        IArtistRepository Artists { get; }
        IAlbumRepository Albums { get; }
        IGenreRepository Genres { get; }
        IPlaylistRepository Playlists { get; }

        Task<int> SaveChangesAsync();
    }
}
