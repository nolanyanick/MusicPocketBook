using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MusicPocketBook.BusinessLayer.ViewModels;
using System.ComponentModel;

namespace MusicPocketBook.BusinessLayer.ViewModels
{
    class BaseViewModel : ObservableObject, IPageViewModel
    {
        private ICommand _changePageCommand;
        string IPageViewModel.Name { get { return "Base"; }}

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

        private IPageViewModel _currentPageViewModel;

        public IPageViewModel CurrentPageViewModel
        {
            get { return _currentPageViewModel; }
            set { _currentPageViewModel = value; }
        }

        private List<IPageViewModel> _pageViewModels;

        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                {
                    _pageViewModels = new List<IPageViewModel>();
                }
                return _pageViewModels;
            }
        }

        public IPageViewModel ChangePageViewModel
        {
            get { return _currentPageViewModel; }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
            {
                PageViewModels.Add(viewModel);
            }

            CurrentPageViewModel = PageViewModels.
                FirstOrDefault(vm => vm == viewModel);
        }



        public BaseViewModel()
        {
            PageViewModels.Add(new PocketBookViewModel());
            PageViewModels.Add(new KeyGenViewModel());

            CurrentPageViewModel = PageViewModels[0];
        }


    }
}
