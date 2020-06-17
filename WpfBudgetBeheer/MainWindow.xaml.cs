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
using WpfBudgetBeheer.ViewModel;


namespace WpfBudgetBeheer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		CategorieViewModel _catViewModel = null;
		TransactieViewModel _trViewModel = null;
		trViewM _viewmod = null;

		public CategorieViewModel CatViewModel 
		{ get { _catViewModel ??= new CategorieViewModel(); return _catViewModel; } 
		  set => _catViewModel = value; 
		}
		
		
		
		public TransactieViewModel TrViewModel
		{
			get { _trViewModel ??= new TransactieViewModel(); return _trViewModel; }
			set => _trViewModel = value;
		}
		public trViewM ViewMod 
		{
			get { _viewmod ??= new trViewM(); return _viewmod; }
			set => _viewmod = value;
		}
		public MainWindow()
		{
			InitializeComponent();
			ViewMod.VulData();
			//CatViewModel.VulData();
			//TrViewModel.VulData();
			//DataContext = TrViewModel;
			//DataContext = this;
			DataContext = ViewMod;


		}

		private void FontSizeEdit_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !IsNumber(e.Text);
		}
		public bool IsNumber(string txt) => int.TryParse(txt, out int i);

		private void ImportButton_Click(object sender, RoutedEventArgs e)
		{
			/*OpenFileDialog openDlg = new OpenFileDialog()
			{
				Filter = "Json files (*.json)|*.json|All files (*.*)|*.*"
			};
			if (openDlg.ShowDialog() == true)
			{
			CatViewModel.ImportFromFile(openDlg.FileName);
			TrViewModel.ImportFromFile(openDlg.FileName);
			}*/


		}

		private void ExportButton_Click(object sender, RoutedEventArgs e)
		{
			
			SaveFileDialog saveDlg = new SaveFileDialog()
			{
				Filter = "Json files (*.json)|*.json|All files (*.*)|*.*"
			};
			if (saveDlg.ShowDialog() == true)
			{
				CatViewModel.ExportToFile(saveDlg.FileName);
			}
			
			/*CatViewModel.ExportToFile(CatViewModel.HoofdCatLijst.categ);
			TrViewModel.ExportToFile(TrViewModel.TrPostLijst.trans);*/
			SaveFileDialog saveDlg1 = new SaveFileDialog()
			{
				Filter = "Json files (*.json)|*.json|All files (*.*)|*.*"
			};
			if (saveDlg.ShowDialog() == true)
			{
				TrViewModel.ExportToFile(saveDlg.FileName);
			}
		}

		private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount > 3)
			{
				//CatViewModel.VulData();
			}
		}



		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void AddHCButton_Click(object sender, RoutedEventArgs e)
		{
			var now = DateTime.Now.Date;
			
			//CatViewModel.addSubCategorie("Corona", "Handschoenen");
			TrViewModel.addTransactiePost(DateTime.Now, "Salarissen", "Loon", 0, 1550.25);
			TrViewModel.addTransactiePost(DateTime.Now, "Vaste Kost Woning", "Huur", 469, 0);
			TrViewModel.addTransactiePost(DateTime.Now, "Nutsvoorzieningen", "Electriciteit", 0,40.50);
			TrViewModel.addTransactiePost(DateTime.Now, "Nutsvoorzieningen", "Internet", 0, 27.80);
		}



		private void DeleteTransactieButton_Click(object sender, RoutedEventArgs e)
		{

				//TrViewModel.deleteTransactiePost(TrViewModel.CurrentTP);

		}


		private void EditTransactieButton_Click(object sender, RoutedEventArgs e)
		{
			if (ViewMod.CurrentTP != null)
			{
				//if (ViewMod.CurrSCLijst != null) { }
				//ViewMod.CurrSCLijst = ViewMod.CurrentTP.HoofdCat.SubCats;
				//ViewMod.CurrSCLijst = new List<string>();
				//foreach (var sc in hc.SubCats) { ViewMod.CurrSCLijst.Add(sc.Naam); }
				//ViewMod.SCCollectieNaam = new ObservableCollection<string>(ViewMod.CurrSCLijstNaam);

			}
		}
	}
}	
