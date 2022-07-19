using EntityFrameworkTest.View_Models;
using System.Windows;

namespace EntityFrameworkTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainView viewModel;

        public MainWindow(MainView mainView)
        {
            InitializeComponent();

            viewModel = mainView;

            DataContext = viewModel;
        }
    }
}
