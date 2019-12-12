using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPocketBook.BusinessLayer.ViewModels
{
    class KeyGenViewModel : ObservableObject, IPageViewModel
    {
        public string Name { get { return "Keys"; } }
    }
}
