using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MusicPocketBook.ViewModels;
using MusicPocketBook.BusinessLayer;
using MusicPocketBook.Models;
using MongoDB.Driver;

namespace MusicPocketBook.ValidationRules
{
    class KeyNameRule : ValidationRule
    {
        private KeyBusiness _keyBusiness;

        public KeyBusiness KeyBusiness
        {
            get { return _keyBusiness; }
            set { _keyBusiness = value; }
        }

        private IMongoDatabase _db;

        public IMongoDatabase Db
        {
            get { return _db; }
            set { _db = value; }
        }

        private IMongoCollection<Keys> _collection;

        public IMongoCollection<Keys> Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }

        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        private List<Keys> _keysCollection;

        public List<Keys> KeysCollection
        {
            get { return _keysCollection; }
            set { _keysCollection = value; }
        }

        private KeyGenViewModel _keyVM;

        public KeyGenViewModel KeyVM
        {
            get { return _keyVM; }
            set { _keyVM = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            _connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(_connectionString);

            Db = client.GetDatabase("pocketBookData");

            _collection = Db.GetCollection<Keys>("keys");
            _keysCollection = _collection.Find(Builders<Keys>.Filter.Empty).ToList();

            try
            {
                if ((string)value != "")
                {
                    foreach (Keys key in _keysCollection)
                    {                        
                        if ((string)value == key.Name)
                        {
                            return new ValidationResult(false, "Name already in database");
                        }
                    }
                }
                else
                {
                    return new ValidationResult(false, "Name cannot be blank");
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ValidationResult.ValidResult;
        }   
    }
}
