using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace MusicPocketBook.Models
{
    public class Keys : ObservableObject
    {
        private int _id;
        private string _name;
        private string _imageFileName;
        private string _imageFilePath;
        private List<int> _chordsId;

        private List<string> _chordName;

        public List<string> ChordName
        {
            get { return _chordName; }
            set { _chordName = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [BsonId]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
       
        public string ImageFileName
        {
            get { return _imageFileName; }
            set { _imageFileName = value; }
        }


        public string ImageFilePath
        {
            get { return _imageFilePath; }
            set { _imageFilePath = value; }
        }


        public List<int> ChordsId
        {
            get { return _chordsId; }
            set { _chordsId = value; }
        }

        public Keys()
        {

        }
    }
}
