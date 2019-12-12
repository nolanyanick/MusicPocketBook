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
    public class ChordBusiness
    {
        #region FIELDS

        private IMongoDatabase _db;
        private string _connectionString;
        private List<Chord> _chordsCollection;
        private IMongoCollection<Chord> _collection;
        private Chord _selectedChord;

        #endregion

        #region PROPERTIES

        public string Name { get { return "Chords"; } }

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

        public List<Chord> ChordsCollection
        {
            get { return _chordsCollection; }
            set { _chordsCollection = value; }
        }

        public IMongoCollection<Chord> Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }

        #endregion

        #region METHODS

        public void AddChord(Chord chord)
        {
            try
            {
                if (chord != null)
                {
                    using (ChordsRepository chordRepository = new ChordsRepository())
                    {
                        chordRepository.Add(chord);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void DeleteChord(Chord chord)
        {
            try
            {
                if (chord != null)
                {
                    using (ChordsRepository chordRepository = new ChordsRepository())
                    { 
                        chordRepository.DeleteChordById(chord.Id);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void UpdateChord(Chord chord)
        {

            try
            {
                if (chord != null)
                {
                    using (ChordsRepository chordRepository = new ChordsRepository())
                    {
                        chordRepository.Update(chord);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Chord> GetChordById(int id)
        {
            List<Chord> chords = null;

            try
            {
                using (ChordsRepository chordRepository = new ChordsRepository())
                {
                    chords = chordRepository.GetChordById(id);
                };
            }
            catch (Exception)
            {

                throw;
            }

            return chords;
        }

        public List<Keys> GetKeys(int id)
        {
            List<Keys> keyFamily = new List<Keys>();
            List<Chord> chords = null;
            List<Keys> keys = null;

            try
            {
                using (ChordsRepository chordsRepository = new ChordsRepository())
                {
                    chords = chordsRepository.GetChordById(id);
                }

                using (KeysRepository keysRepository = new KeysRepository())
                {
                    foreach (int keyId in chords[0].KeysId)
                    {
                        keys = keysRepository.GetKeyById(keyId);
                        keyFamily.Add(keys[0]);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return keyFamily;
        }

        public void InitializeComponent()
        {
            _connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(_connectionString);

            Db = client.GetDatabase("pocketBookData");

            _collection = Db.GetCollection<Chord>("chords");

            if (_collection.CountDocuments(Builders<Chord>.Filter.Empty) == 0)
            {
                _collection.InsertMany(SeedData.GenerateListOfChords());
            }

            _chordsCollection = _collection.Find(Builders<Chord>.Filter.Empty).ToList();
        }


        #endregion

        #region CONSTRUCTORS

        public ChordBusiness()
        {
            InitializeComponent();
        }

        #endregion
    }
}
