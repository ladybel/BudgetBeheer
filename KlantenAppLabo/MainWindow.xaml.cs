using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using KlantenAppWPF.ViewModel;

namespace KlantenAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewMod _viewM = null;
        
        ViewMod ViewM
        {
            get { _viewM ??= new ViewMod(); return _viewM; }
            set => _viewM = value;
        }
        public MainWindow()
        {
            InitializeComponent();
            ViewM.KlantenLijst.Import();
            ViewM.BookingLijst.Import();
            DataContext = ViewM;


        }

        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddHCButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
