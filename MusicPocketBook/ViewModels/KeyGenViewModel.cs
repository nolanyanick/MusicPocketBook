using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPocketBook.Models;
using MongoDB.Driver;
using MusicPocketBook.DAL;
using MusicPocketBook.BusinessLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace MusicPocketBook.ViewModels
{
    class KeyGenViewModel : ObservableObject, IPageViewModel
    {
        #region COMMANDS

        public ICommand AddKeyCommand { get { return new RelayCommand(OnAddKey); } }
        public ICommand DeleteKeyCommand { get { return new RelayCommand(OnDeleteKey); } }
        public ICommand UpdateKeyCommand { get { return new RelayCommand(OnUpdateKey); } }
        public ICommand SaveKeyCommand { get { return new RelayCommand(OnSaveKey); } }
        public ICommand CancelKeyCommand { get { return new RelayCommand(OnCancelKey); } }

        #endregion

        #region FIELDS

        private IMongoDatabase _db;
        private string _connectionString;
        private ObservableCollection<Keys> _keysCollection;
        private IMongoCollection<Keys> _collection;
        private Keys _selectedKey;
        private KeyBusiness _keyBusiness;
        private Keys _addEditKey;
        private List<Chord> _allChords;
        private List<Chord> _chordsToAddOrEdit;
        private bool _showSaveButton;
        private bool _showAddButton;

        #endregion

        #region PROPERTIES

        public string vmName { get { return "Key Generator"; } }

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

        public ObservableCollection<Models.Keys> KeysCollection
        {
            get { return _keysCollection; }
            set { _keysCollection = value; }
        }

        public IMongoCollection<Models.Keys> Collection
        {
            get { return _collection; }
            set { _collection = value; }
        }

        public KeyBusiness KeyBusiness
        {
            get { return _keyBusiness; }
            set { _keyBusiness = value; }
        }

        public Models.Keys SelectedKey
        {
            get { return _selectedKey; }
            set
            {
                if (_selectedKey == value)
                {
                    return;
                }
                _selectedKey = value;
                OnPropertyChanged("SelectedKey");
            }
        }

        public Models.Keys AddEditKey
        {
            get { return _addEditKey; }
            set
            {
                _addEditKey = value;
                OnPropertyChanged("AddEditKey");
            }
        }

        public List<Chord> AllChords
        {
            get { return _allChords; }
            set { _allChords = value; }
        }

        public List<Chord> ChordsToAddOrEdit
        {
            get { return _chordsToAddOrEdit; }
            set
            {
                _chordsToAddOrEdit = value;
                OnPropertyChanged("ChordsToAddEdit");
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

        #endregion

        #region METHODS

        public void InitializeComponent()
        {
            _keyBusiness = new KeyBusiness();
            _showSaveButton = false;

            _connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(_connectionString);

            Db = client.GetDatabase("pocketBookData");

            _collection = Db.GetCollection<Models.Keys>("keys");

            _keysCollection = new ObservableCollection<Keys>(_collection.Find(Builders<Keys>.Filter.Empty).ToList());
        }

        private void OnAddKey()
        {
            PrepareChordFamily();

            if (_addEditKey.Name == "" || _addEditKey.Id == 0)
            {
               System.Windows.MessageBox.Show(
                    "Please enter an Id and a name first.", "Nothing to Add",
                    System.Windows.MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            if (_addEditKey != null)
            {
                _keyBusiness.AddKey(_addEditKey);
                _keysCollection.Add(AddEditKey);

                System.Windows.MessageBox.Show(
                $"{_addEditKey.Name} has been added.", "Key Added",
                System.Windows.MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ResetAddEditViewFields();
        }

        private void OnDeleteKey()
        {
            if (_selectedKey != null)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show(
                    $"Are you sure you want to delete the {_selectedKey.Name} key?", "Delete Key", 
                    System.Windows.MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

                if (messageBoxResult == MessageBoxResult.OK)
                {
                    //
                    // delete character from persistence
                    //
                    _keyBusiness.DeleteKey(_selectedKey);
                    _keysCollection.Remove(_selectedKey);
                }
            }
        }

        private void OnUpdateKey()
        {                      
            ShowAddButton = false;
            ShowSaveButton = true;
            AddEditKey = _selectedKey;
            OnPropertyChanged("AddEditKey");

            for (int i = 0; i < _chordsToAddOrEdit.Count; i++)
            {
                _chordsToAddOrEdit[i] = _allChords.Find(x => x.Id == _addEditKey.ChordsId[i]);
                OnPropertyChanged("ChordsToAddOrEdit");
            }        
        }

        private void OnSaveKey()
        {
            UpdateChordFamily();

            _keyBusiness.UpdateKey(_addEditKey);
            _keysCollection.Remove(_selectedKey);
            _keysCollection.Add(_addEditKey);
            _selectedKey = _keysCollection.FirstOrDefault();
            
            ResetAddEditViewFields();
            ShowAddButton = true;
            ShowSaveButton = false;            
        } 

        private void OnCancelKey()
        {
            ResetAddEditViewFields();
            ShowAddButton = true;
            ShowSaveButton = false;
        }

        public void PrepareChordFamily()
        {
            List<string> chordNames = new List<string>();
            List<int> chordIds = new List<int>();

            foreach (Chord chord in ChordsToAddOrEdit)
            {
                if (_addEditKey == null)
                {
                    _addEditKey = new Models.Keys();
                }

                if (chord.Name == null || chord.Name == "")
                {
                    chord.Name = "N/A";
                }

                chordNames.Add(chord.Name);
                chordIds.Add(chord.Id);
            }

            _addEditKey.ChordName = chordNames;
            _addEditKey.ChordsId = chordIds;
        }

        public void UpdateChordFamily()
        {
            _addEditKey.ChordsId = new List<int>();
            _addEditKey.ChordName = new List<string>();

            foreach (Chord chord in _chordsToAddOrEdit)
            {
                _addEditKey.ChordsId.Add(chord.Id);
            }

            foreach (Chord chord in _chordsToAddOrEdit)
            {
                _addEditKey.ChordName.Add(chord.Name);
            }
        }

        public void ResetAddEditViewFields()
        {
            _addEditKey = new Models.Keys();
            _addEditKey.Name = "";
            _addEditKey.Id = 0;

            for (int i = 0; i < ChordsToAddOrEdit.Count; i++)
            {
                ChordsToAddOrEdit[i++] = new Chord
                {

                };
                OnPropertyChanged("ChordsToAddOrEdit");
            }

            OnPropertyChanged("AddEditKey");
        }

        private List<Chord> GetAllChords()
        {
            List<Chord> listOfAllChords = new List<Chord>();
            IMongoCollection<Chord> dbChords;

            dbChords = Db.GetCollection<Chord>("chords");
            listOfAllChords = dbChords.Find(Builders<Chord>.Filter.Empty).ToList();

            return listOfAllChords;
        }

        #endregion

        #region CONSTRUCTORS

        public KeyGenViewModel()
        {
            InitializeComponent();
            _selectedKey = _keysCollection.FirstOrDefault();
            _addEditKey = new Models.Keys();       
            _allChords = GetAllChords();
            _showAddButton = true;

            //instantiate list of chords for CRUD ops
            //each chord is 'empty'
            _chordsToAddOrEdit = new List<Chord>()
            {
                //1
                new Chord
                {

                },

                //2
                new Chord
                {

                },

                //3
                new Chord
                {

                },

                //4
                new Chord
                {

                },

                //5
                new Chord
                {

                },

                //6
                new Chord
                {

                },

                //7
                new Chord
                {

                },
            };
        }

        #endregion
    }
}