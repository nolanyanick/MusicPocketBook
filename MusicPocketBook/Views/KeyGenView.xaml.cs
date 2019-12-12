using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MusicPocketBook.Views
{
    /// <summary>
    /// Interaction logic for KeyGenView.xaml
    /// </summary>
    public partial class KeyGenView : UserControl
    {
        public KeyGenView()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = (ListBox)sender;

            listBox.ScrollIntoView(listBox.SelectedItem);
        }
    }
}
