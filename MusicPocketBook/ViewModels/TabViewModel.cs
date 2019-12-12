using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPocketBook.ViewModels
{
    class TabViewModel : ObservableObject, IPageViewModel
    {
        public string vmName { get { return "Tabs"; } }
    }
}
