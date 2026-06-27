using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Interfaces
{
    public interface IUserLikedSongRepository
    {
        Task<List<UserLikedSong>> GetByUserIdAsync(int userId);
        Task AddAsync(UserLikedSong likedSong);
        Task RemoveAsync(int userId, int songId);
        Task<bool> IsLikedAsync(int userId, int songId);
    }
}
