using GroupProject.Item;
using GroupProject.Search;
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
using System.Reactive;
using GroupProject.Repositories;
using GroupProject.Main;

namespace GroupProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private wndSearch _searchWindow;
        private wndItems _itemsWindow;

        private readonly MainViewModel _mainViewModel;
        public MainWindow()
        {
            _itemsWindow = new wndItems();
            _searchWindow = new wndSearch();
            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;

            InitializeComponent();
            _searchWindow.CancelObservable.Subscribe((x) =>
            {
                _searchWindow.Hide();
                this.Show();
            });
            
            _itemsWindow.CancelObservable.Subscribe(x =>
            {
                _itemsWindow.Hide();
                this.Show();
            });
        }

        private void NavigateToSearch(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this._searchWindow.Show();
        }
        private void NavigateToItems(object sender, RoutedEventArgs e)
        {
            this.Hide();
            _itemsWindow.Show();
        }
    }
}
