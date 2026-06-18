using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Rythm.Application.Features.Songs.DTOs;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Infrastructure.Search
{
    public class ElasticsearchService : ISearchService
    {
        private readonly ElasticsearchClient _client;
        private const string IndexName = "songs";

        public ElasticsearchService(IConfiguration configuration)
        {
            var url = configuration["ElasticsearchSettings:Url"]!;
            var settings = new ElasticsearchClientSettings(new Uri(url)).DefaultIndex(IndexName);
            _client = new ElasticsearchClient(settings);
        }

        public async Task IndexSongAsync(int songId, string title, string artistName, string? albumTitle, string? genreName)
        {
            var song = new SongSearchResult
            {
                SongId = songId,
                Title = title,
                ArtistName = artistName,
                AlbumTitle = albumTitle,
                GenreName = genreName
            };

            await _client.IndexAsync(song, i => i.Index(IndexName).Id(songId.ToString()));
        }

        public async Task DeleteSongAsync(int songId)
        {
            await _client.DeleteAsync(IndexName, songId.ToString());
        }

        public async Task<List<SongSearchResult>> SearchSongsAsync(string query)
        {
            var response = await _client.SearchAsync<SongSearchResult>(s => s.Index(IndexName)
            .Query(q => q
                .MultiMatch(m => m
                    .Fields(new[] { "title", "artistName", "albumTitle", "genreName" })
                    .Query(query)
                    .Fuzziness(new Fuzziness("AUTO"))
                )
            )
        );

            return response.Documents.ToList();
        }
    }
}
