using Rythm.Application.Features.Songs.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Interfaces
{
    public interface ISearchService
    {
        Task IndexSongAsync(int songId, string title, string artistName, string? albumTitle, string? genreName);
        Task DeleteSongAsync(int songId);
        Task<List<SongSearchResult>> SearchSongsAsync(string query);
    }
}
