using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPocketBook.Models;
using MusicPocketBook.DAL;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MusicPocketBook.BusinessLayer;

namespace MusicPocketBook.DAL.Repository
{
    class ChordsRepository : IChordsRepository, IDisposable
    {
        #region FIELDS

        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Chord> _chordsCollection;

        #endregion

        #region PROPERTIES


        #endregion

        #region METHODS

        public void Add(Chord chord)
        {
            _chordsCollection.InsertOne(chord);
        }

        public List<Chord> GetChordById(int id)
        {
            var filter = Builders<Chord>.Filter.Eq("_id", id);
            var result = _chordsCollection.Find(filter).ToList();

            return result;
        }

        public async Task<List<Chord>> GetAll()
        {
            return await _chordsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Chord>> GetChordsByField(string fieldName, string fieldValue)
        {
            var filter = Builders<Chord>.Filter.Eq(fieldName, fieldValue);
            var result = await _chordsCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<List<Chord>> GetChords(int startingFrom, int count)
        {
            var result = await _chordsCollection.Find(new BsonDocument())
            .Skip(startingFrom)
            .Limit(count)
            .ToListAsync();

            return result;
        }

        public bool Update(Chord chord)
        {
            var filter = Builders<Chord>.Filter.Eq("_id", chord.Id);
            var update = Builders<Chord>.Update.Set("Name", chord.Name)
                .Set("ChordType", chord.ChordType)
                .Set("KeysId", chord.KeysId)
                .Set("ImageFilePath", chord.ImageFilePath)
                .Set("ImageFileName", chord.ImageFileName);


            var result = _chordsCollection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        public void DeleteChordById(int id)
        {
            var filter = Builders<Chord>.Filter.Eq("_id", id);
            _chordsCollection.DeleteOne(filter);
        }

        public void Dispose()
        {
            _chordsCollection = null;
        }

        #endregion

        #region CONSTRUCTORS

        public ChordsRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("pocketBookData");
            _chordsCollection = _database.GetCollection<Chord>("chords");
        }


        #endregion
    }
}
