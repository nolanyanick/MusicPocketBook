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


    public interface IChordsRepository
    {
        void Add(Chord chord);
        Task<List<Chord>> GetAll();
        Task<List<Chord>> GetChordsByField(string fieldName, string fieldValue);
        Task<List<Chord>> GetChords(int startingFrom, int count);
        bool Update(Chord chord);
        void DeleteChordById(int id);
        List<Chord> GetChordById(int id);
    }
}
