using Microsoft.Win32;
using System.Windows.Input;

namespace EntityFrameworkTest.View_Models
{
    public class MainView : ViewModel
    {
        private string _filename;

        public ICommand BrowseFileCommand { get; set; }

        public MainView()
        {
            BrowseFileCommand = new RelayCommand(BrowseFile, CanBrowse);
        }



        private void BrowseFile(object value)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        }
        private bool CanBrowse(object value)
        {
            return true;
        }

        public string FileName
        {
            get { return _filename; }
            set
            {
                _filename = value;
                OnPropertyChanged();
            }
        }
    }
}
