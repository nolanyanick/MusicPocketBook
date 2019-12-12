using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MusicPocketBook.Models;
using MusicPocketBook.ViewModels;
using MusicPocketBook.DAL;
using MusicPocketBook.DAL.Repository;

namespace MusicPocketBook.BusinessLayer
{
    public class KeyBusiness
    {
        #region FIELDS

        private IMongoDatabase _db;
        private string _connectionString;
        private List<Keys> _keysCollection;
        private IMongoCollection<Keys> _collection;
        private Keys _selectedKey;

        #endregion

        #region PROPERTIES

        public string Name { get { return "Key Generator"; } }

        public IMongoDatabase Db
        {
            get { return _db; }
            set { _db = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public List<Keys> KeysCollection
        {
            get { return _keysCollection; }
            set { _keysCollection = value; }
        }

        public IMongoCollection<Keys> Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }

        #endregion

        #region METHODS

        public void AddKey(Keys key)
        {
            try
            {
                if (key != null)
                {
                    using (KeysRepository keyRepository = new KeysRepository())
                    {
                        keyRepository.Add(key);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteKey(Keys key)
        {
            try
            {
                if (key != null)
                {
                    using (KeysRepository keyRepository = new KeysRepository())
                    {
                        keyRepository.DeleteKeyById(key.Id);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateKey(Keys key)
        {

            try
            {
                if (key != null)
                {
                    using (KeysRepository keyRepository = new KeysRepository())
                    {
                        keyRepository.Update(key);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Keys> GetKeyById(int id)
        {
            List<Keys> keys = null;

            try
            {
                using (KeysRepository keyRepository = new KeysRepository())
                {
                    keys = keyRepository.GetKeyById(id);
                };
            }
            catch (Exception)
            {

                throw;
            }

            return keys;
        }

        public List<Chord> GetChordFamliy(int id)
        {
            List<Chord> chordFamily = new List<Chord>();
            List<Keys> keys = null;
            List<Chord> chords = null;

            try
            {
                using (KeysRepository keysRepository = new KeysRepository())
                {
                    keys = keysRepository.GetKeyById(id);
                }

                using (ChordsRepository chordsRepository = new ChordsRepository())
                {
                    foreach (int chordId in keys[0].ChordsId)
                    {
                        chords = chordsRepository.GetChordById(chordId);
                        chordFamily.Add(chords[0]);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return chordFamily;
        }

        public void InitializeComponent()
        {
            _connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(_connectionString);

            Db = client.GetDatabase("pocketBookData");

            _collection = Db.GetCollection<Keys>("keys");

            if (_collection.CountDocuments(Builders<Keys>.Filter.Empty) == 0)
            {
                _collection.InsertManyAsync(SeedData.GenerateListOfKeys());
            }            

                _keysCollection = _collection.Find(Builders<Keys>.Filter.Empty).ToList();
        }


        #endregion

        #region CONSTRUCTORS

        public KeyBusiness()
        {
            InitializeComponent();
        }

        #endregion
    }
}
