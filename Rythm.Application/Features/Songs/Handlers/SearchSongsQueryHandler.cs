using MediatR;
using Rythm.Application.Features.Songs.DTOs;
using Rythm.Application.Features.Songs.Queries;
using Rythm.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Songs.Handlers
{
    public class SearchSongsQueryHandler : IRequestHandler<SearchSongsQuery, List<SongSearchResult>>
    {
        private readonly ISearchService _searchService;

        public SearchSongsQueryHandler(ISearchService searchService)
        {
            _searchService = searchService;
        }

        public async Task<List<SongSearchResult>> Handle(SearchSongsQuery request, CancellationToken cancellationToken)
        {
            return await _searchService.SearchSongsAsync(request.Query);
        }
    }
}
