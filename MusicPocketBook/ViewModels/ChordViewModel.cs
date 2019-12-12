using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPocketBook.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MusicPocketBook.DAL;
using MusicPocketBook.BusinessLayer;
using System.Windows.Input;
using System.Windows;

namespace MusicPocketBook.ViewModels
{
    class ChordViewModel : ObservableObject, IPageViewModel
    {
        #region ENUMS

        public enum OperationStatus
        {
            None,
            Adding, 
            Editing
        }

        #endregion

        #region COMMANDS

        public ICommand AddChordCommand { get { return new RelayCommand(OnAddChord); } }
        public ICommand DeleteChordCommand { get { return new RelayCommand(OnDeleteChord); } }
        public ICommand UpdateChordCommand { get { return new RelayCommand(OnUpdateChord); } }
        public ICommand SaveChordCommand { get { return new RelayCommand(OnSaveChord); } }
        public ICommand CancelChordCommand { get { return new RelayCommand(OnCancelChord); } }

        #endregion

        #region FIELDS

        private bool _idReadOnly;

        public bool IdReadOnly
        {
            get { return _idReadOnly; }
            set 
            { 
                _idReadOnly = value;
                OnPropertyChanged("IdReadOnly");
            }
        }


        private IMongoDatabase _db;
        private string _connectionString;
        private List<Chord> _chordsCollection;
        private IMongoCollection<Chord> _collection;
        private Chord _selectedChord;
        private List<Keys> _allKeys;
        private ChordBusiness _chordBusiness;
        private Chord _addEditChord;
        private List<Keys> _keysToAddOrEdit;
        private bool _showSaveButton;
        private bool _showAddButton;
        private List<Chord.Type> _allChordTypes;
        private OperationStatus _operationStatus; 

        #endregion

        #region PROPERTIES

        public string vmName { get { return "Chords"; } }

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

        public ChordBusiness ChordBusiness
        {
            get { return _chordBusiness; }
            set { _chordBusiness = value; }
        }


        public Chord SelectedChord
        {
            get { return _selectedChord; }
            set
            {
                if (_selectedChord == value)
                {
                    return;
                }
                _selectedChord = value;
                _addEditChord = _selectedChord;
                OnPropertyChanged("SelectedChord");
                OnPropertyChanged("AddEditChord");
            }
        }

        public Chord AddEditChord
        {
            get { return _addEditChord; }
            set
            {
                _addEditChord = value;
                OnPropertyChanged("AddEditChord");
            }
        }

        public List<Keys> KeysToAddOrEdit
        {
            get { return _keysToAddOrEdit; }
            set
            {
                _keysToAddOrEdit = value;
                OnPropertyChanged("KeysToAddEdit");
            }
        }

        public bool ShowSaveButton
        {
            get { return _showSaveButton; }
            set
            {
                _showSaveButton = value;
                OnPropertyChanged("ShowSaveButton");
            }
        }

        public bool ShowAddButton
        {
            get { return _showAddButton; }
            set
            {
                _showAddButton = value;
                OnPropertyChanged("ShowAddButton");
            }
        }

        public List<Chord.Type> AllChordTypes
        {
            get { return _allChordTypes; }
            set { _allChordTypes = value; }
        }

        public List<Keys> AllKeys
        {
            get { return _allKeys; }
            set { _allKeys = value; }
        }


        #endregion

        #region METHODS

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

        public List<Chord> GetListOfChords()
        {            
            _collection = Db.GetCollection<Chord>("chords");
            List<Chord> chords = _collection.Find(Builders<Chord>.Filter.Empty).ToList();

            return chords;                        
        }

        private void OnAddChord()
        {
            PrepareKeyFamily();

            if (_addEditChord.Name == "" || _addEditChord.Id == 0)
            {
                System.Windows.MessageBox.Show(
                     "Please enter an Id and a Name first.", "Nothing to Add",
                     System.Windows.MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            _idReadOnly = false;
            _showAddButton = false;
            _showSaveButton = true;
            _operationStatus = OperationStatus.Adding;
            ResetAddEditViewFields();
        }

        private void OnDeleteChord()
        {
            if (_selectedChord != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                    $"Are you sure you want to delete the {_selectedChord.Name} key?", "Delete Chord",
                    System.Windows.MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                if (messageBoxResult == MessageBoxResult.OK)
                {
                    //
                    // delete chord from persistence
                    //
                    _chordBusiness.DeleteChord(_selectedChord);
                    _chordsCollection.Remove(_selectedChord);
                    OnPropertyChanged("ChordsCollection");
                }
            }
        }

        private void OnUpdateChord()
        {
            ShowAddButton = false;
            ShowSaveButton = true;
            AddEditChord = _selectedChord;
            OnPropertyChanged("AddEditChord");

            for (int i = 0; i < _keysToAddOrEdit.Count; i++)
            {
                _keysToAddOrEdit[i] = _allKeys.Find(x => x.Id == _addEditChord.KeysId[i]);
                OnPropertyChanged("ChordsToAddOrEdit");
            }

            _operationStatus = OperationStatus.Editing;
        }

        private void OnSaveChord()
        {
            UpdateChordFamily();

            if (_operationStatus == OperationStatus.Adding)
            {
                if (_addEditChord != null)
                {
                    _chordBusiness.AddChord(_addEditChord);
                    _chordsCollection.Add(AddEditChord);

                    System.Windows.MessageBox.Show(
                    $"{_addEditChord.Name} has been added.", "Chord Added",
                    System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (_operationStatus == OperationStatus.Editing)
            {
                _chordBusiness.UpdateChord(_addEditChord);
                _chordsCollection.Remove(_selectedChord);
                _chordsCollection.Add(_addEditChord);
                _selectedChord = _chordsCollection.FirstOrDefault();
            }

            ResetAddEditViewFields();
            ShowAddButton = true;
            ShowSaveButton = false;
        }

        private void OnCancelChord()
        {
            ResetAddEditViewFields();
            ShowAddButton = true;
            ShowSaveButton = false;
            _addEditChord = _selectedChord;
            OnPropertyChanged("AddEditChord");
        }

        public void PrepareKeyFamily()
        {
            List<string> keyNames = new List<string>();
            List<int> keyIds = new List<int>();

            foreach (Keys key in KeysToAddOrEdit)
            {
                if (_addEditChord == null)
                {
                    _addEditChord = new Chord();
                }

                if (key.Name == null || key.Name == "")
                {
                    key.Name = "N/A";
                }

                keyNames.Add(key.Name);
                keyIds.Add(key.Id);
            }

            //_addEditChord.ChordName = chordNames;
            _addEditChord.KeysId = keyIds;
        }

        public void UpdateChordFamily()
        {
            _addEditChord.KeysId = new List<int>();
           // _addEditChord.ChordName = new List<string>();

            foreach (Keys key in _keysToAddOrEdit)
            {
                _addEditChord.KeysId.Add(key.Id);
            }

            //foreach (Chord chord in _keysToAddOrEdit)
            //{
            //    _addEditChord.ChordName.Add(chord.Name);
            //}
        }

        public void ResetAddEditViewFields()
        {
            _addEditChord = new Chord();
            _addEditChord.Name = "";
            _addEditChord.Id = 0;

            for (int i = 0; i < KeysToAddOrEdit.Count; i++)
            {
                KeysToAddOrEdit[i++] = new Keys
                {

                };
                OnPropertyChanged("KeysToAddOrEdit");
            }

            OnPropertyChanged("AddEditChord");
        }

        private List<Keys> GetAllKeys()
        {
            List<Keys> listOfAllKeys = new List<Keys>();
            IMongoCollection<Keys> dbKeys;

            dbKeys = Db.GetCollection<Keys>("keys");
            listOfAllKeys = dbKeys.Find(Builders<Keys>.Filter.Empty).ToList();

            return listOfAllKeys;
        }

        private void UpdateViewChordToSelected()
        {
            _addEditChord = new Chord();
            _addEditChord.Id = _selectedChord.Id;
            _addEditChord.Name = _selectedChord.Name;
            _addEditChord.ImageFileName = _selectedChord.ImageFileName;
            _addEditChord.ImageFilePath = _selectedChord.ImageFilePath;
            OnPropertyChanged("AddEditChord");
        }

        public List<Chord.Type> GetAllChordTypes()
        {
            List<Chord.Type> types = new List<Chord.Type>();

            types.Add(Chord.Type.None);
            types.Add(Chord.Type.Major);
            types.Add(Chord.Type.Minor);
            types.Add(Chord.Type.SharpDiminished);
            types.Add(Chord.Type.SharpMinor);
            types.Add(Chord.Type.Flat);
            types.Add(Chord.Type.Diminished);

            return types;
        }

        #endregion

        #region CONSTRUCTORS

        public ChordViewModel()
        {
            InitializeComponent();
            _chordBusiness = new ChordBusiness();
            _selectedChord = _chordsCollection.FirstOrDefault();
            _addEditChord = _selectedChord;
            _allKeys = GetAllKeys();
            _showAddButton = true;
            _keysToAddOrEdit = new List<Keys>();
            _allChordTypes = GetAllChordTypes();
            _idReadOnly = true;
        }

        #endregion
    }
}