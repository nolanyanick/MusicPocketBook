using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPocketBook.Models;
using MongoDB;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Bson;
namespace MusicPocketBook.DAL.Repository
{
    

    public interface IKeysRepository
    {
        void Add(Keys key);
        Task<List<Keys>> GetAll();
        Task<List<Keys>> GetKeysByField(string fieldName, string fieldValue);
        Task<List<Keys>> GetKeys(int startingFrom, int count);
        bool Update(Keys key);
        void DeleteKeyById(int id);
        List<Keys> GetKeyById(int id);
    }
}
