using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroupProject.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        public IObservable<EventPattern<RoutedEventArgs>> CancelObservable { get; }

        public wndSearch()
        {
            InitializeComponent();

            CancelObservable = Observable.FromEventPattern<RoutedEventHandler,RoutedEventArgs>(
                x => this.btnCancel.Click += x, 
                x=> this.btnCancel.Click -= x);
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
           //The selected invoice will be sent back to the main window. This will be sent by using a setter or function of some type. 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            // wndSearch.Close(); this will be used to close the current window and show the MainWindow again. 
        }
    }
}
