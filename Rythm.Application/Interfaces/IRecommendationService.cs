using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Interfaces
{
    public interface IRecommendationService
    {
        Task<List<int>> GetRecommendedSongIdsAsync(int userId, int count = 10);
        Task TrainModelAsync();
    }
}
