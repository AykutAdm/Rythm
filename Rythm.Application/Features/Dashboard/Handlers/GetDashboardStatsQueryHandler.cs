using MediatR;
using Microsoft.AspNetCore.Identity;
using Rythm.Application.Features.Dashboard.DTOs;
using Rythm.Application.Features.Dashboard.Queries;
using Rythm.Application.Interfaces;
using Rythm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Application.Features.Dashboard.Handlers
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public GetDashboardStatsQueryHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var songs = await _unitOfWork.Songs.GetAllAsync();
            var artists = await _unitOfWork.Artists.GetAllAsync();
            var albums = await _unitOfWork.Albums.GetAllAsync();
            var genres = await _unitOfWork.Genres.GetAllAsync();
            var playlists = await _unitOfWork.Playlists.GetAllAsync();
            var users = _userManager.Users.ToList();

            return new DashboardStatsDto
            {
                TotalSongs = songs.Count,
                TotalArtists = artists.Count,
                TotalAlbums = albums.Count,
                TotalGenres = genres.Count,
                TotalPlaylists = playlists.Count,
                TotalUsers = users.Count
            };
        }
    }
}
