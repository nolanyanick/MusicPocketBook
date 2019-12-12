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
    class KeysRepository : IKeysRepository , IDisposable
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<Keys> _keysCollection;

        //add keys
        public void Add(Keys key)
        {
            _keysCollection.InsertOne(key);
        }

        public List<Keys> GetKeyById(int id)
        {            
            var filter = Builders<Keys>.Filter.Eq("_id", id);
            var result = _keysCollection.Find(filter).ToList();

            return result;
        }
      
        public async Task<List<Keys>> GetAll()
        {
            return await _keysCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Keys>> GetKeysByField(string fieldName, string fieldValue)
        {
            var filter = Builders<Keys>.Filter.Eq(fieldName, fieldValue);
            var result = await _keysCollection.Find(filter).ToListAsync();

            return result;
        }

        public async Task<List<Keys>> GetKeys(int startingFrom, int count)
        {
            var result = await _keysCollection.Find(new BsonDocument())
            .Skip(startingFrom)
            .Limit(count)
            .ToListAsync();

            return result;
        }

        public bool Update(Keys key)
        {
            var filter = Builders<Keys>.Filter.Eq("_id", key.Id);
            var update = Builders<Keys>.Update.Set("Name", key.Name)
                .Set("ChordsId", key.ChordsId)
                .Set("ChordName", key.ChordName);


            var result = _keysCollection.UpdateOne(filter, update);

            return result.ModifiedCount != 0;
        }

        public void DeleteKeyById(int id)
        {
            var filter = Builders<Keys>.Filter.Eq("_id", id);
            _keysCollection.DeleteOne(filter);
        }

        public KeysRepository()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _database = _client.GetDatabase("pocketBookData");
            _keysCollection = _database.GetCollection<Keys>("keys");
        }

        public void Dispose()
        {
            _keysCollection = null;
        }
    }
}
