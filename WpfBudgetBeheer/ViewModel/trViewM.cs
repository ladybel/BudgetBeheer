using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Syntra.Data.Models;
using WpfBudgetBeheer.ViewModel;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows;
using WpfBudgetBeheer.Tools;
using DevExpress.Office;
using System.DirectoryServices;

namespace WpfBudgetBeheer.ViewModel
{
	public class trViewM : INotifyPropertyChanged
	{
		#region Info
		/// Class 
		#endregion Info
		#region Definitions 
		#endregion Definitions
		#region Fields
		public event PropertyChangedEventHandler PropertyChanged;
		TransactiePosten _tpLijst = null;
		ObservableCollection<TransactiePost> _tpCollectie = null;

		

		//CategorieViewModel cvm = new CategorieViewModel();
		HoofdCategorieLijst _hcLijst = null;
		List<string> _hcLijstNaam = null;
		List<SubCategorie> _currSCLijst = null;
		List<string> _currSCLijstNaam = null;
		ObservableCollection<HoofdCategorie> _hcCollectie = null;
		ObservableCollection<SubCategorie> _scCollectie = null;
		ObservableCollection<string> _hcCollectieNaam = null;
		ObservableCollection<string> _scCollectieNaam = null;

		TransactiePost _currentTP = null;
		HoofdCategorie _currentHC = null;
		SubCategorie _currentSC = null;
		#endregion Fields
		#region Constructors
		#endregion Constructors
		#region Properties
		public TransactiePosten TrPostLijst { get { _tpLijst ??= new TransactiePosten(); return _tpLijst; } set => _tpLijst = value; }

		public ObservableCollection<TransactiePost> TPCollectie
		{
			get
			{
				_tpCollectie ??= new ObservableCollection<TransactiePost>(TrPostLijst.Members); return _tpCollectie;
			}
			set => TrPostLijst.Members = value.ToList();
		}
		public TransactiePost CurrentTP 
		{
			get 
			{
				if (_currentTP != null) { 
				if (_currentTP.SubCat == null)
				{

					//	_currentTP.SubCat = _currentTP.HoofdCat.SubCats[0];
					
					
				}
			}
				return _currentTP;
			}
			set 
			{ 
				_currentTP = value; 
				RaisePropertyChanged("CurrentTP"); 
			} 
		}

		public HoofdCategorieLijst HoofdCatLijst { get { _hcLijst ??= new HoofdCategorieLijst(); return _hcLijst; } set => _hcLijst = value; }

		
		public List<SubCategorie> CurrSCLijst 
		{ 
			get
			{
				_currSCLijst = new List<SubCategorie>();

					if (CurrentHC != null) { 
						_currSCLijst = CurrentHC.SubCats;
					}
					else if (CurrentTP != null)
					{
						_currSCLijst = CurrentTP.HoofdCat.SubCats ;
					}
				
				
				return _currSCLijst; 
			} 
			set => _currSCLijst = value;
		}
		public List<string> CurrSCLijstNaam 
		{ 
			get
			{   
				_currSCLijstNaam ??= new List<string>();

				if (_currSCLijstNaam.Count == 0) { 
					foreach (var item in CurrSCLijst)
					{
						_currSCLijstNaam.Add(item.Naam);
					}
				}

				
				return _currSCLijstNaam;
			} 
			set => _currSCLijstNaam = value;
		}
		public List<string> HCLijstNaam
		{
			get
			{

					_hcLijstNaam ??= new List<string>();
				if (_hcLijstNaam.Count == 0) { 
					foreach (var item in HoofdCatLijst.Members)
					{
						_hcLijstNaam.Add(item.Naam);
					}
				}

				return _hcLijstNaam;
			}
			set => _hcLijstNaam = value;
		}
		public ObservableCollection<SubCategorie> SCCollectie
		{
			get
			{
				_scCollectie= new ObservableCollection<SubCategorie>(CurrSCLijst);
				return _scCollectie;
			}
			set => _scCollectie = value;
		}
		public ObservableCollection<HoofdCategorie> HCCollectie
		{
			get
			{
				
				_hcCollectie ??= new ObservableCollection<HoofdCategorie>(HoofdCatLijst.Members); 
				return _hcCollectie;
			}
			set => HoofdCatLijst.Members = value.ToList();
		}
		
		public ObservableCollection<string> HCCollectieNaam
		{
			get
			{

				_hcCollectieNaam ??= new ObservableCollection<string>(HCLijstNaam);
				
				return _hcCollectieNaam;
			}
		
				set => _hcCollectieNaam = value;
			

		}
		public ObservableCollection<string> SCCollectieNaam 
		{ 
			get
			{
				_scCollectieNaam= new ObservableCollection<string>(CurrSCLijstNaam); 
				return _scCollectieNaam;
			}
			set => _scCollectieNaam = value;
		} 
		public HoofdCategorie CurrentHC 
			{
			get => _currentHC; 
			set 
			{ 
				_currentHC = value; 
				RaisePropertyChanged("CurrentHC"); 
			} 
		}
		public SubCategorie CurrentSC 
		{ 
			get => _currentSC; 
			set 
			{ 
				_currentSC = value; 
				RaisePropertyChanged("CurrentSC"); 
			} 
		}
		#endregion Properties
		#region Methods
		//protected void RaisePropertyChanged([CallerMemberName] string property = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));


		public bool addTransactiePost(DateTime datum, SubCategorie sc, string comment, double debet, double credit)
		{
			HoofdCategorie hc = HoofdCatLijst.getHoofdCategorie(sc);
			TrPostLijst.addTransactiePost(datum, hc, sc, comment, debet, credit);
			//ResetTPList();
			return true;
		}
		public bool addTransactiePost(DateTime datum, string sc_naam, string comment, double debet, double credit)
		{
			//SubCategorie sc = cvm.HoofdCatLijst.getSubCategorie(sc_naam);
			//HoofdCategorie hc = cvm.HoofdCatLijst.getHoofdCategorie(sc_naam);
			SubCategorie sc = HoofdCatLijst.getSubCategorie(sc_naam);
			HoofdCategorie hc = HoofdCatLijst.getHoofdCategorie(sc_naam);
			TrPostLijst.addTransactiePost(datum, hc, sc, comment, debet, credit); ;
			//ResetTPList();
			return true;
		}

		/*public HoofdCategorie getHoofdCategorie(SubCategorie sc)
		{
			return cvm.HoofdCatLijst.getHoofdCategorie(sc);
		}
		public bool deleteTransactiePost(TransactiePost tp)
		{
			TrPostLijst.deleteTransactiePost(tp);
			ResetTPList();
			return true;
		}
		*/

		public void ResetTPList()
		{
			//CurrentTP = null;
			
			TPCollectie.Clear();
			foreach (var tp in TrPostLijst.Members) { TPCollectie.Add(tp); }


		}

		public bool UpdateTPList()
		{
			if (TPCollectie?.Count > 0)
			{
				TrPostLijst.Members = new List<TransactiePost>(TPCollectie);
				return true;
			}
			return false;
		}
		internal void VulData()
		{
			TrPostLijst.VulData();
			//ResetTPList();
			HoofdCatLijst.VulData();
			//ResetSCList();
			//ResetHCList();


		}

		protected void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		public bool Import(string json)
		{
			var ok = TrPostLijst.Import(json);
			ResetTPList();
			return ok;
		}
		public string Export()
		{
			UpdateTPList();
			return TrPostLijst?.Export();
		}
		public bool ImportFromFile(string path)
		{
			if (File.Exists(path))
			{
				return Import(File.ReadAllText(path)) == true;
			}
			return false;
		}
		public bool ExportToFile(string path)
		{
			try
			{
				File.WriteAllText(path, Export());
				return File.Exists(path);
			}
			catch { }
			return false;
		}
		public bool addHoofdCategorie(string naam)
		{
			HoofdCatLijst.addHoofdCategorie(naam);
			ResetHCList();
			return true;

		}
		public bool addHoofdCategorie(string hc_naam, string sc_naam)
		{
			HoofdCatLijst.addHoofdCategorie(hc_naam, sc_naam);
			ResetHCList();
			return true;


		}
		public bool addHoofdCategorie(HoofdCategorie hc)
		{
			HoofdCatLijst.addHoofdCategorie(hc);
			ResetHCList();
			return true;
		}
		public bool deleteHoofdCategorie(HoofdCategorie hc)
		{
			HoofdCatLijst.deleteHoofdCategorie(hc);
			ResetHCList();
			return true;
		}
		public bool deleteHoofdCategorie(string naam)
		{
			var hc = HCCollectie.Where(t => t.Naam == naam).FirstOrDefault();
			if (hc != null)
			{

				HoofdCatLijst.deleteHoofdCategorie(hc);
				ResetHCList();
				return true;
			}
			else return false;
		}
		public bool renameHoofdCategorie(HoofdCategorie hc, string naam)
		{
			HoofdCatLijst.renameHoofdCategorie(hc, naam);
			ResetHCList();
			return true;
		}
		public bool renameHoofdCategorie(string oud, string nieuw)
		{
			var hc = HCCollectie.Where(t => t.Naam == oud).FirstOrDefault();
			if (hc != null)
			{

				HoofdCatLijst.renameHoofdCategorie(hc, nieuw);
				ResetHCList();
				return true;
			}
			else return false;
		}
		public bool addSubCategorie(string hc_naam, string sc_naam)
		{
			var hc = HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{

				hc.addSubCategorie(sc_naam);
				ResetHCList();
				return true;
			}
			else return false;

		}
		public bool addSubCategorie(HoofdCategorie hc, string sc_naam)
		{
			hc.addSubCategorie(sc_naam);
			ResetHCList();
			return true;
		}

		public bool addSubCategorie(HoofdCategorie hc, SubCategorie sc)
		{
			hc.addSubCategorie(sc);
			ResetHCList();
			return true;
		}
		public bool addSubCategorie(string hc_naam, SubCategorie sc)
		{
			var hc = HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{

				hc.addSubCategorie(sc);
				ResetHCList();
				return true;
			}
			else return false;
		}
		public bool deleteSubCategorie(HoofdCategorie hc, SubCategorie sc)
		{
			hc.deleteSubCategorie(sc);
			ResetHCList();
			return true;
		}
		public bool deleteSubCategorie(string hc_naam, string sc_naam)
		{
			var hc = HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{

				hc.deleteSubCategorie(sc_naam);
				ResetHCList();
				return true;
			}
			else return false;

		}
		public bool renameSubCategorie(HoofdCategorie hc, SubCategorie sc, string nieuw)
		{
			hc.renameSubCategorie(sc, nieuw);
			ResetHCList();
			return true;
		}
		/*public HoofdCategorie getHoofdCategorie(string sc_naam)
		{
			return HoofdCatLijst.getHoofdCategorie(sc_naam);
		}
		public HoofdCategorie getHoofdCategorie(SubCategorie sc)
		{
			return HoofdCatLijst.getHoofdCategorie(sc);
		}
		public SubCategorie getSubCategorie(string sc_naam)
		{

			return HoofdCatLijst.getSubCategorie(sc_naam);
		}
		*/
		public void ResetHCList()
		{
			CurrentHC = null;
			HCCollectie.Clear();
			foreach (var hc in HoofdCatLijst.Members.Where(t => t.Deleted == false)) { HCCollectie.Add(hc); }
			//foreach (var hc in HoofdCatLijst.Members) { HCCollectie.Add(hc); }

		}
		public void ResetSCList()
		{
			CurrentSC = null;
			SCCollectie.Clear();
			if (CurrentHC != null)
			{
				foreach (var sc in CurrentHC.SubCats)
				{
					SCCollectie.Add(sc);
				}
			}
			else if(CurrentTP!=null)
			{
				foreach (var sc in CurrentTP.HoofdCat.SubCats)
				{
					SCCollectie.Add(sc);
				}
			}
		}
		public bool UpdateHCList()
		{
			if (HCCollectie?.Count > 0)
			{
				HoofdCatLijst.Members = new List<HoofdCategorie>(HCCollectie);
				return true;
			}
			return false;
		}
		/*internal void VulData()
		{
			HoofdCatLijst.VulData();
			ResetHCList();

		}
		protected void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		public bool Import(string json)
		{
			var ok = HoofdCatLijst.Import(json);
			ResetHCList();
			return ok;
		}
		public string Export()
		{
			UpdateHCList();
			return HoofdCatLijst?.Export();
		}
		public bool ImportFromFile(string path)
		{
			if (File.Exists(path))
			{
				return Import(File.ReadAllText(path)) == true;
			}
			return false;
		}
		public bool ExportToFile(string path)
		{

			try
			{
				File.WriteAllText(path, Export());
				return File.Exists(path);
			}
			catch { }
			return false;
		}*/
		#endregion Methods      



	}
}
