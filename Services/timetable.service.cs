using MongoDB.Driver;
using TimeFourthe.Entities;
using TimeFourthe.Configurations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TimeFourthe.Services {
    public class TimetableService {
        private readonly IMongoCollection<TimetableData> _timetableCollection;

        public TimetableService(IOptions<MongoDbSettings> mongoDbSettings) {
            var client = new MongoClient(mongoDbSettings.Value.ConnectionString);
            var database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
            _timetableCollection = database.GetCollection<TimetableData>(mongoDbSettings.Value.CollectionName[2]);
        }

        public async Task<List<TimetableData>> GetTimetableDataAsync() =>
            await _timetableCollection.Find(timetableData => true).ToListAsync();

        public async Task InsertTimetableDataAsync(TimetableData timetableData) {
            await _timetableCollection.InsertOneAsync(timetableData);
        }
    }
}
