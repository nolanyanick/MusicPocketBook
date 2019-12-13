using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MusicPocketBook.ViewModels;
using MongoDB.Driver;
using MusicPocketBook.Models;
using MusicPocketBook.DAL;

namespace MusicPocketBook
{
    /// <summary>
    /// Credit for this code goes to Rachel Lim: https://rachel53461.wordpress.com/2011/12/18/navigation-with-mvvm-2/
    /// Minor edits were made by Nolan Yanick
    /// </summary>
    class BaseViewModel : ObservableObject
    {
        #region Fields        

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;
        private IMongoDatabase _db;
        private IMongoCollection<Keys> _keysCollection;
        private IMongoCollection<Chord> _chordsCollection;
        private string _connectionString;

        #endregion

        #region Constructor

        public BaseViewModel()
        {
            //init database
            InitializeDatabase();

            // Add available pages
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new KeyGenViewModel());
            PageViewModels.Add(new ChordViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #endregion

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if (_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        public IMongoDatabase Db
        {
            get { return _db; }
            set { _db = value; }
        }

        public IMongoCollection<Keys> KeysCollection
        {
            get { return _keysCollection; }
            set { _keysCollection = value; }
        }

        public IMongoCollection<Chord> ChordsCollection
        {
            get { return _chordsCollection; }
            set { _chordsCollection = value; }
        }

        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        public void InitializeDatabase()
        {
            //connect to mongoDB
            _connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(_connectionString);

            //create/get database
            Db = client.GetDatabase("pocketBookData");

            //get collections
            _keysCollection = Db.GetCollection<Keys>("keys");
            _chordsCollection = Db.GetCollection<Chord>("chords");

            //add seed data to keys collection if there are no documents present
            if (_keysCollection.CountDocuments(Builders<Keys>.Filter.Empty) == 0)
            {
                _keysCollection.InsertMany(SeedData.GenerateListOfKeys());
            }

            //add seed data to chords collection if there are no documents present
            if (_chordsCollection.CountDocuments(Builders<Chord>.Filter.Empty) == 0)
            {
                _chordsCollection.InsertMany(SeedData.GenerateListOfChords());
            }
        }

        #endregion
    }
}
