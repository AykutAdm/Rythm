using Microsoft.AspNetCore.Hosting;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Rythm.Application.Interfaces;
using Rythm.Infrastructure.Recommendation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Infrastructure.Recommendation
{
    public class MlNetRecommendationService : IRecommendationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _modelPath;
        private readonly MLContext _mlContext = new();

        public MlNetRecommendationService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _modelPath = Path.Combine(env.ContentRootPath, "MLModels", "song-recommender.zip");
        }

        public async Task<List<int>> GetRecommendedSongIdsAsync(int userId, int count = 10)
        {
            // Train Model
            if (!File.Exists(_modelPath))
            {
                await TrainModelAsync();
            }

            var model = _mlContext.Model.Load(_modelPath, out _);
            var engine = _mlContext.Model.CreatePredictionEngine<SongRating, SongRatingPrediction>(model);


            var userHistory = await _unitOfWork.ListeningHistories.GetByUserIdAsync(userId);
            var listenedIds = userHistory.Select(h => h.SongId).ToHashSet();
            var allSongs = await _unitOfWork.Songs.GetAllAsync();
            var candidates = allSongs.Where(s => !listenedIds.Contains(s.SongId));


            var scored = candidates
                .Select(song =>
                {
                    var prediction = engine.Predict(new SongRating
                    {
                        UserId = (uint)userId,
                        SongId = (uint)song.SongId,
                        Label = 1f
                    });
                    return new { song.SongId, prediction.Score };
                })
                .OrderByDescending(x => x.Score)
                .Take(count)
                .Select(x => x.SongId)
                .ToList();
            return scored;
        }


        public async Task TrainModelAsync()
        {
            var histories = await _unitOfWork.ListeningHistories.GetAllAsync();

            var trainingData = histories
                .GroupBy(h => new { h.AppUserId, h.SongId })
                .Select(g => new SongRating
                {
                    UserId = (uint)g.Key.AppUserId,
                    SongId = (uint)g.Key.SongId,
                    Label = g.Count()
                }).ToList();

            if (trainingData.Count < 10) return;
            var dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = nameof(SongRating.UserId),
                MatrixRowIndexColumnName = nameof(SongRating.SongId),
                LabelColumnName = nameof(SongRating.Label),
                NumberOfIterations = 20,
                ApproximationRank = 64
            };

            var pipeline = _mlContext.Recommendation().Trainers.MatrixFactorization(options);
            var model = pipeline.Fit(dataView);

            // save
            Directory.CreateDirectory(Path.GetDirectoryName(_modelPath)!);
            _mlContext.Model.Save(model, dataView.Schema, _modelPath);
        }
    }
}
