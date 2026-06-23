using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Interfaces
{
    public interface IPlaylistRepository
    {
        Task<List<Playlist>> GetAllAsync();
        Task<Playlist> GetByIdAsync(int id);
        Task AddAsync(Playlist playlist);
        Task UpdateAsync(Playlist playlist);
        Task DeleteAsync(int id);

        Task<List<Playlist>> GetByUserIdAsync(int userId);
        Task AddSongAsync(PlaylistSong playlistSong);
        Task RemoveSongAsync(int playlistId, int songId);
    }
}
