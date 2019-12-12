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
    class PocketBookViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get { return "Pocket"; } }
        public PocketBookViewModel()
        {
            

        }
    }
}
