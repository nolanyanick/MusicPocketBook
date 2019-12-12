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
    public class KeyIdRule : ValidationRule
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





        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            _connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(_connectionString);

            Db = client.GetDatabase("pocketBookData");

            _collection = Db.GetCollection<Keys>("keys");
            _keysCollection = _collection.Find(Builders<Keys>.Filter.Empty).ToList();

            int intResult = 0;
            try
            {
                if (((string)value).Length > 0)
                {                    
                intResult = Int32.Parse((String)value);
                }
                
                if ((string)value == "")
                {
                    return new ValidationResult(false, "Id cannot be blank");
                }
                else
                {
                    foreach (Keys key in _keysCollection)
                    {
                        if ((string)value == key.Id.ToString())
                        {
                            return new ValidationResult(false, "Id already in database");
                        }
                    }
                }

                
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Please enter a valid number");
                throw;
            }

            return ValidationResult.ValidResult;
        }
    }   
}
