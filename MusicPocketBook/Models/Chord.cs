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
    public class Chord
    {
        public enum Type
        {
            None,
            Major, 
            Minor, 
            SharpDiminished,
            SharpMinor,
            Flat,
            Diminished
        }        

        private int _id;
        private string _name;
        private string _imageFileName;
        private string _imageFilePath;
        private List<int> _keysId;
        private Type _type;

        private List<string> _keyNames;

        public List<string> KeyNames
        {
            get { return _keyNames; }
            set { _keyNames = value; }
        }


        public Type ChordType
        {
            get { return _type; }
            set { _type = value; }
        }

        [BsonId]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        public List<int> KeysId
        {
            get { return _keysId; }
            set { _keysId = value; }
        }
    }
}
