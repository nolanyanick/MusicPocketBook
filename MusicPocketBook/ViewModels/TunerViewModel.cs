using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPocketBook.ViewModels
{
    class TunerViewModel : ObservableObject, IPageViewModel
    {
        public string vmName { get { return "Tuner"; } }
    }
}
